using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class UserContestService : IUserContestService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IContestService contestService;

        public UserContestService(PhotoContestContext dbContext, IContestService contestService)
        {
            this.dbContext = dbContext;
            this.contestService = contestService;
        }

        public async Task CalculatePointsAsync()
        {
            //var contests = await this.dbContext.Contests.Where(c => c.Status.Name == "Finished").ToListAsync();
            var contests = await this.contestService.GetAllFinishedContestsAsync();
            foreach (var contest in contests)
            {
                foreach (var photo in this.dbContext.Photos.Where(p=>p.ContestId==contest.Id)) // TODO
                {
                    CalculatePhotoPoints(photo);
                }
                //Sort photos for first, second or third place
                var sortedPhotos = contest.Photos.OrderByDescending(p => p.AllPoints).ToList();

                var FirstPlacePhotos = sortedPhotos.Where(p => p.AllPoints == sortedPhotos[0].AllPoints).ToList();
                var countFirstPlacePhotos = FirstPlacePhotos.Count();
                sortedPhotos.RemoveRange(0, countFirstPlacePhotos);

                var SecondPlacePhotos = sortedPhotos.Where(p => p.AllPoints == sortedPhotos[0].AllPoints).ToList();
                var countSecondPlacePhotos = SecondPlacePhotos.Count();
                sortedPhotos.RemoveRange(0, countSecondPlacePhotos);

                var ThirdPlacePhotos = sortedPhotos.Where(p => p.AllPoints == sortedPhotos[0].AllPoints).ToList();
                var countThirdPlacePhotos = ThirdPlacePhotos.Count();

                //Points of users in first place
                await CalculatePointsForUsersFirstPlaceAsync(countFirstPlacePhotos, contest, FirstPlacePhotos, SecondPlacePhotos);
                //Points of users in second place
                await CalculatePointsForUsersSecondPlaceAsync(countSecondPlacePhotos, contest, SecondPlacePhotos);
                //Points of users in third place
                await CalculatePointsForUsersThirdPlaceAsync(countThirdPlacePhotos, contest, ThirdPlacePhotos);

                //Add points to users depending on open/invitational
                if (contest.IsOpen)
                {
                    foreach (var userContest in await this.dbContext.UserContests.Where(uc => uc.ContestId == contest.Id).ToListAsync())
                    {
                        userContest.Points += 1;
                    }
                }
                else
                {
                    foreach (var userContest in await this.dbContext.UserContests.Where(uc => uc.ContestId == contest.Id).ToListAsync())
                    {
                        userContest.Points += 3;
                    }
                }
            }
            //calculate points for each user participating in a Contest
            foreach (var userContest in await this.dbContext.UserContests.ToListAsync())
            {
                userContest.User.OverallPoints += userContest.Points;
            }
        }
        private void CalculatePhotoPoints(Photo photo)
        {
            foreach (var review in photo.Reviews)
            {
                photo.AllPoints += review.Score;
            }
            photo.AllPoints /= photo.Reviews.Count();
        }
        private async Task CalculatePointsForUsersFirstPlaceAsync(int countFirstPlacePhotos, Contest contest, List<Photo> FirstPlacePhotos, List<Photo> SecondPlacePhotos)
        {
            if (countFirstPlacePhotos > 1)
            {
                foreach (var photo in FirstPlacePhotos)
                {
                    var userContest = await this.dbContext.UserContests.FirstOrDefaultAsync(uc => uc.UserId == photo.UserId && uc.ContestId == contest.Id);
                    userContest.Points = 40;
                }
            }
            else if (countFirstPlacePhotos == 1)
            {
                var photo = FirstPlacePhotos.First();
                var userContest = await this.dbContext.UserContests.FirstOrDefaultAsync(uc => uc.UserId == photo.UserId && uc.ContestId == contest.Id);
                var photoSecondPlace = SecondPlacePhotos.FirstOrDefault();
                if (photoSecondPlace != null)
                {
                    if (photo.AllPoints >= photoSecondPlace.AllPoints * 2)
                    {
                        userContest.Points = 75;
                    }
                    else
                    {
                        userContest.Points = 50;
                    }
                }
                else
                {
                    userContest.Points = 50;
                }
            }
        }
        private async Task CalculatePointsForUsersSecondPlaceAsync(int countSecondPlacePhotos, Contest contest, List<Photo> SecondPlacePhotos)
        {
            if (countSecondPlacePhotos > 1)
            {
                foreach (var photo in SecondPlacePhotos)
                {
                    var userContest = await this.dbContext.UserContests.FirstOrDefaultAsync(uc => uc.UserId == photo.UserId && uc.ContestId == contest.Id);
                    userContest.Points = 25;
                }
            }
            else if (countSecondPlacePhotos == 1)
            {
                var photo = SecondPlacePhotos.First();
                var userContest = await this.dbContext.UserContests.FirstOrDefaultAsync(uc => uc.UserId == photo.UserId && uc.ContestId == contest.Id);
                userContest.Points = 35;
            }
        }
        private async Task CalculatePointsForUsersThirdPlaceAsync(int countThirdPlacePhotos, Contest contest, List<Photo> ThirdPlacePhotos)
        {
            if (countThirdPlacePhotos > 1)
            {
                foreach (var photo in ThirdPlacePhotos)
                {
                    var userContest = await this.dbContext.UserContests.FirstOrDefaultAsync(uc => uc.UserId == photo.UserId && uc.ContestId == contest.Id);
                    userContest.Points = 10;
                }
            }
            else if (countThirdPlacePhotos == 1)
            {
                var photo = ThirdPlacePhotos.First();
                var userContest = await this.dbContext.UserContests.FirstOrDefaultAsync(uc => uc.UserId == photo.UserId && uc.ContestId == contest.Id);
                userContest.Points = 20;
            }
        }
    }
}

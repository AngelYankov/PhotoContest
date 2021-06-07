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
        /// <summary>
        /// Get all user contests.
        /// </summary>
        /// <returns>Returns all user contests.</returns>
        public async Task<List<UserContest>> GetAllUserContestsAsync()
        {
            return await this.dbContext.UserContests.Include(uc=>uc.User).Include(uc=>uc.Contest).ToListAsync();
        }
        /// <summary>
        /// Calculate points for each user by their photo points calculated by reviews' points.
        /// </summary>
        /// <returns>Calculate all points.</returns>
        public async Task CalculatePointsAsync()
        {
            //var contests = await this.dbContext.Contests.Where(c => c.Status.Name == "Finished").ToListAsync();
            var allFinishedContests = await this.contestService.GetAllFinishedContestsAsync();
            var contests = allFinishedContests.Where(c => c.IsCalculated == false);
            foreach (var contest in contests)
            {
                var photos = await this.dbContext.Photos.Include(p => p.Contest).Include(p => p.User).Where(p => p.ContestId == contest.Id).ToListAsync();
                foreach (var photo in photos) // TODO
                {
                    await CalculatePhotoPoints(photo);
                }
                //Sort photos for first, second or third place
                var sortedPhotos = photos.OrderByDescending(p => p.AllPoints).ToList();

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
                var userContests = await this.dbContext.UserContests.Include(uc=>uc.User).Where(uc => uc.ContestId == contest.Id && uc.IsAdded == false).ToListAsync();
                if (contest.IsOpen)
                {
                    foreach (var userContest in userContests)
                    {
                        userContest.Points += 1;
                    }
                }
                else
                {
                    foreach (var userContest in userContests)
                    {
                        userContest.Points += 3;
                    }
                }
                contest.IsCalculated = true;
                //calculate points for each user participating in a Contest
                foreach (var userContest in userContests)
                {
                    var user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Id == userContest.UserId);
                    user.OverallPoints += userContest.Points;
                    userContest.IsAdded = true;
                }
            }
            await this.dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Calculate points for a photo.
        /// </summary>
        /// <param name="photo">Photo for which points will be calculated.</param>
        /// <returns>Calculate points.</returns>
        private async Task CalculatePhotoPoints(Photo photo)
        {
            var reviews = await this.dbContext.Reviews.Include(r => r.Photo).Include(r => r.Evaluator).Where(r => r.PhotoId == photo.Id).ToListAsync();
            if (reviews.Count() == 0 )
            {
                photo.AllPoints = 3;
                return;
            }
            foreach (var review in reviews)
            {
                photo.AllPoints += review.Score;
            }
            photo.AllPoints = photo.AllPoints / reviews.Count();   
            // await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Calculate points for users on first place.
        /// </summary>
        /// <param name="countFirstPlacePhotos">Count of how many users on first place.</param>
        /// <param name="contest">Contest of the first place photos.</param>
        /// <param name="FirstPlacePhotos">First place photos.</param>
        /// <param name="SecondPlacePhotos">Second place photos.</param>
        /// <returns>Calculate points.</returns>
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
                var photoFirstPlace = FirstPlacePhotos.FirstOrDefault();
                var userContest = await this.dbContext.UserContests.Include(uc => uc.User).Include(uc => uc.Contest).FirstOrDefaultAsync(uc => uc.UserId == photoFirstPlace.UserId && uc.ContestId == contest.Id);
                var photoSecondPlace = SecondPlacePhotos.FirstOrDefault();
                if (photoSecondPlace != null)
                {
                    if (photoFirstPlace.AllPoints >= photoSecondPlace.AllPoints * 2)
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
            //await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Calculate points for users on second place.
        /// </summary>
        /// <param name="countSecondPlacePhotos">Count of the second place users.</param>
        /// <param name="contest">Contest of the second place photos.</param>
        /// <param name="SecondPlacePhotos">Second place photos.</param>
        /// <returns>Calculate points.</returns>
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
                var photoSecondPlace = SecondPlacePhotos.First();
                var userContest = await this.dbContext.UserContests.FirstOrDefaultAsync(uc => uc.UserId == photoSecondPlace.UserId && uc.ContestId == contest.Id);
                userContest.Points = 35;
            }
            //await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Calculate points for users on third place.
        /// </summary>
        /// <param name="countThirdPlacePhotos">Count of third place photos.</param>
        /// <param name="contest">Contest of the third place photos.</param>
        /// <param name="ThirdPlacePhotos">Third place photos.</param>
        /// <returns>Calculate points.</returns>
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
                var photoThirdPlace = ThirdPlacePhotos.First();
                var userContest = await this.dbContext.UserContests.FirstOrDefaultAsync(uc => uc.UserId == photoThirdPlace.UserId && uc.ContestId == contest.Id);
                userContest.Points = 20;
            }
            //await this.dbContext.SaveChangesAsync();
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
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
                if (contest.IsOpen)
                {
                    foreach (var userContest in await this.dbContext.UserContests.Where(uc => uc.ContestId == contest.Id && uc.IsAdded == false).ToListAsync())
                    {
                        userContest.Points += 1;
                    }
                }
                else
                {
                    foreach (var userContest in await this.dbContext.UserContests.Where(uc => uc.ContestId == contest.Id && uc.IsAdded == false).ToListAsync())
                    {
                        userContest.Points += 3;
                    }
                }
                contest.IsCalculated = true;
                //calculate points for each user participating in a Contest
                foreach (var userContest in await this.dbContext.UserContests.Include(uc => uc.User).Where(uc => uc.ContestId == contest.Id 
                                                                                                              && uc.IsAdded == false).ToListAsync())
                {
                    var user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Id == userContest.UserId);
                    user.OverallPoints += userContest.Points;
                    userContest.IsAdded = true;
                }
            }
            await this.dbContext.SaveChangesAsync();
        }
        private async Task CalculatePhotoPoints(Photo photo)
        {
            var reviews = await this.dbContext.Reviews.Include(r => r.Photo).Include(r => r.Evaluator).Where(r => r.PhotoId == photo.Id).ToListAsync();
            foreach (var review in reviews)
            {
                photo.AllPoints += review.Score;
            }
            if (reviews.Count() != 0)
            {
                photo.AllPoints = photo.AllPoints / reviews.Count();
            }

            // await this.dbContext.SaveChangesAsync();
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

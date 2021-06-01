﻿using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class GetAllFinished_Should
    {
        [TestMethod]
        public async Task ReturnAll_FinishedContests()
        {
            var options = Utils.GetOptions(nameof(ReturnAll_FinishedContests));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetAllFinishedContestsAsync();

                Assert.AreEqual(actContext.Contests
                                          .Where(c => c.Status.Name == "Finished")
                                          .Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Finished")), string.Join(",", result));
            }
        }
    }
}
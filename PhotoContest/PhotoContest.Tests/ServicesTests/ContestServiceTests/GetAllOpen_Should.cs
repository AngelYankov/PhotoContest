using Microsoft.AspNetCore.Http;
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
    public class GetAllOpen_Should
    {
        [TestMethod]
        public async Task Return_AllOpenContests_Phase1()
        {
            var options = Utils.GetOptions(nameof(Return_AllOpenContests_Phase1));

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

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
                var contestService = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var result = await sut.GetAllOpenAsync("Phase 1");

                Assert.AreEqual(actContext.Contests
                                          .Where(c => c.Status.Name == "Phase 1")
                                          .Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Contests
                                                           .Where(c => c.Status.Name == "Phase 1")
                                                           .Select(c => new ContestDTO(c))), string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_AllOpenContests_Phase2()
        {
            var options = Utils.GetOptions(nameof(Return_AllOpenContests_Phase2));

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

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
                var contestService = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var result = await sut.GetAllOpenAsync("Phase 2");

                Assert.AreEqual(actContext.Contests
                                          .Where(c => c.Status.Name == "Phase 2")
                                          .Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Contests
                                                           .Where(c => c.Status.Name == "Phase 2")
                                                           .Select(c => new ContestDTO(c))), string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_AllOpenContests_Finished()
        {
            var options = Utils.GetOptions(nameof(Return_AllOpenContests_Finished));

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

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
                var contestService = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var result = await sut.GetAllOpenAsync("Finished");

                Assert.AreEqual(actContext.Contests
                                          .Where(c => c.Status.Name == "Finished")
                                          .Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Contests
                                                           .Where(c => c.Status.Name == "Finished")
                                                           .Select(c => new ContestDTO(c))), string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task ThrowsWhen_Phase_IsWrong()
        {
            var options = Utils.GetOptions(nameof(ThrowsWhen_Phase_IsWrong));

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

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
                var contestService = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetAllOpenAsync("Wrong"));
            }
        }
    }
}


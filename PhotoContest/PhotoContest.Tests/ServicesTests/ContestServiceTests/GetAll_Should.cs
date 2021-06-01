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
    public class GetAll_Should
    {
        [TestMethod]
        public async Task Return_AllContests()
        {
            var options = Utils.GetOptions(nameof(Return_AllContests));

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
                var result = await sut.GetAllAsync();

                Assert.AreEqual(actContext.Contests.Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Contests.Select(c => new ContestDTO(c))), string.Join(",", result));
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class FindContestByName_Should
    {
        [TestMethod]
        public async Task Return_Correct_Contest()
        {
            var options = Utils.GetOptions(nameof(Return_Correct_Contest));

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var result = await sut.FindContestByNameAsync(actContext.Contests.First().Name);
                Assert.AreEqual(result.Id, actContext.Contests.First().Id);
                Assert.AreEqual(result.Name, actContext.Contests.First().Name);
                Assert.AreSame(result, actContext.Contests.First());
            }
        }

        [TestMethod]
        public async Task ThrowsWhen_Contest_NotFound()
        {
            var options = Utils.GetOptions(nameof(ThrowsWhen_Contest_NotFound));

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.FindContestByNameAsync("wrong"));
            }
        }
    }
}

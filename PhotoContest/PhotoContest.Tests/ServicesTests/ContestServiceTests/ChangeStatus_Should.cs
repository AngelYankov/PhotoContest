using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class ChangeStatus_Should
    {
        [TestMethod]
        public async Task ChangeStatusTo_Phase1()
        {
            var options = Utils.GetOptions(nameof(ChangeStatusTo_Phase1));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>();
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, userService.Object, categoryService, userManager, signManager);
                var contest = actContext.Contests.Include(c=>c.Status).First();
                contest.Phase1 = new DateTime(2021, 06, 05, 9, 0, 0);
                contest.Phase2 = new DateTime(2021, 06, 12, 9, 0, 0);
                contest.Finished = new DateTime(2021, 06, 12, 19, 0, 0);
                await sut.ChangeStatus();
                Assert.AreEqual(contest.Status.Name, "Phase 1");
            }
        }

        [TestMethod]
        public async Task ChangeStatusTo_Phase2()
        {
            var options = Utils.GetOptions(nameof(ChangeStatusTo_Phase2));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>();
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, userService.Object, categoryService, userManager, signManager);
                var contest = actContext.Contests.Include(c => c.Status).First();
                contest.Phase1 = new DateTime(2021, 06, 01, 9, 0, 0);
                contest.Phase2 = new DateTime(2021, 06, 05, 19, 0, 0);
                contest.Finished = new DateTime(2021, 06, 06, 15, 0, 0);
                await sut.ChangeStatus();
                Assert.AreEqual(contest.Status.Name, "Phase 2");
            }
        }

        [TestMethod]
        public async Task ChangeStatusTo_Finished()
        {
            var options = Utils.GetOptions(nameof(ChangeStatusTo_Finished));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>();
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, userService.Object, categoryService, userManager, signManager);
                var contest = actContext.Contests.Include(c => c.Status).First();
                contest.Phase1 = new DateTime(2021, 06, 01, 9, 0, 0);
                contest.Phase2 = new DateTime(2021, 06, 04, 19, 0, 0);
                contest.Finished = new DateTime(2021, 06, 05, 15, 0, 0);
                await sut.ChangeStatus();
                Assert.AreEqual(contest.Status.Name, "Finished");
            }
        }
    }
}

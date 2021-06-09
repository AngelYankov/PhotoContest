using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

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
                var sut = new ContestService(actContext, contextAccessor, userService.Object, categoryService, userManager, signManager);
                var contest = actContext.Contests.Include(c=>c.Status).First();
                contest.Phase1 = DateTime.UtcNow;
                contest.Phase2 = DateTime.UtcNow.AddDays(2);
                contest.Finished = DateTime.UtcNow.AddHours(60);
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
                var sut = new ContestService(actContext, contextAccessor, userService.Object, categoryService, userManager, signManager);
                var contest = actContext.Contests.Include(c => c.Status).First();
                contest.Phase1 = DateTime.UtcNow.AddDays(-2);
                contest.Phase2 = DateTime.UtcNow;
                contest.Finished = DateTime.UtcNow.AddHours(5);
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
                var sut = new ContestService(actContext, contextAccessor, userService.Object, categoryService, userManager, signManager);
                var contest = actContext.Contests.Include(c => c.Status).First();
                contest.Phase1 = DateTime.UtcNow.AddDays(-5);
                contest.Phase2 = DateTime.UtcNow.AddHours(-5);
                contest.Finished = DateTime.UtcNow;
                await sut.ChangeStatus();
                Assert.AreEqual(contest.Status.Name, "Finished");
            }
        }
    }
}

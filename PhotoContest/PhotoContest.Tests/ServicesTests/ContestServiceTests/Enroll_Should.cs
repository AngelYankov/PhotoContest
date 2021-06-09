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

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class Enroll_Should
    {
        [TestMethod]
        public async Task ReturnTrue_IfUser_EnrolledSuccessfully()
        {
            var options = Utils.GetOptions(nameof(ReturnTrue_IfUser_EnrolledSuccessfully));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>();
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var context = new Mock<HttpContext>();
            contextAccessor.Setup(a => a.HttpContext).Returns(context.Object);

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();

                var userToEnroll = arrContext.Users.Skip(2).First();
                userManager.Setup(u => u.GetUserName(signManager.Context.User)).Returns(userToEnroll.UserName);
                userService.Setup(u => u.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToEnroll));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestToEnroll = actContext.Contests.Last();
                var userToEnroll = actContext.Users.Skip(4).First().UserContests;
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService, userManager.Object, signManager);
                var result = await sut.EnrollAsync(contestToEnroll.Name);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_User_AlreadyEnrolled()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_User_AlreadyEnrolled));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>();
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var context = new Mock<HttpContext>();
            contextAccessor.Setup(a => a.HttpContext).Returns(context.Object);

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.SaveChangesAsync();

                var userToEnroll = arrContext.Users.Skip(2).First();
                userManager.Setup(u => u.GetUserName(signManager.Context.User)).Returns(userToEnroll.UserName);
                userService.Setup(u => u.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToEnroll));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestToEnroll = actContext.Contests.First();
                var userToEnroll = actContext.Users.Skip(2).First();
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService, userManager.Object, signManager);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.EnrollAsync(contestToEnroll.Name));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_User_JuryForContest()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_User_JuryForContest));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>();
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var context = new Mock<HttpContext>();
            contextAccessor.Setup(a => a.HttpContext).Returns(context.Object);

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.SaveChangesAsync();

                var userToEnroll = arrContext.Users.Skip(8).First();
                userManager.Setup(u => u.GetUserName(signManager.Context.User)).Returns(userToEnroll.UserName);
                userService.Setup(u => u.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToEnroll));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestToEnroll = actContext.Contests.Last();
                var userToEnroll = actContext.Users.Skip(8).First();
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService, userManager.Object, signManager);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.EnrollAsync(contestToEnroll.Name));
            }
        }
    }
}

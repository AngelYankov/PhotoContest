using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class ChooseJury_Should
    {
        [TestMethod]
        public async Task ReturnTrue_IfUser_ChosenSuccessfully()
        {
            var options = Utils.GetOptions(nameof(ReturnTrue_IfUser_ChosenSuccessfully));

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
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();

                var userToChoose = arrContext.Users.Skip(8).First();
                userService.Setup(u => u.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToChoose));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var userToChoose = actContext.Users.Skip(8).First().UserName;
                var sut = new ContestService(actContext, contextAccessor, userService.Object, categoryService, userManager, signManager);
                var result = await sut.ChooseJuryAsync(actContext.Contests.First().Name, userToChoose);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_User_HasLesserRank()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_User_HasLesserRank));

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
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();

                var userToChoose = arrContext.Users.Skip(2).First();
                userService.Setup(u => u.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToChoose));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var userToChoose = actContext.Users.Skip(2).First().UserName;
                var sut = new ContestService(actContext, contextAccessor, userService.Object, categoryService, userManager, signManager);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.ChooseJuryAsync(actContext.Contests.First().Name, userToChoose));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_User_AlreadyJury()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_User_AlreadyJury));

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
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.SaveChangesAsync();

                var userToChoose = arrContext.Users.Skip(8).First();
                userService.Setup(u => u.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToChoose));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var userToChoose = actContext.Users.Skip(8).First().UserName;
                var sut = new ContestService(actContext, contextAccessor, userService.Object, categoryService, userManager, signManager);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.ChooseJuryAsync(actContext.Contests.Last().Name, userToChoose));
            }
        }
    }
}

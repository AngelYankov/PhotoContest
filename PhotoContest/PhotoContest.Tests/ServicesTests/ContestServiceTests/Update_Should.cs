using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public async Task Return_UpdatedContest()
        {
            var options = Utils.GetOptions(nameof(Return_UpdatedContest));
            var updateContestDTO = new Mock<UpdateContestDTO>().Object;
            updateContestDTO.Name = "UpdateTest";
            updateContestDTO.CategoryName = "Cars";
            updateContestDTO.IsOpen = true;
            updateContestDTO.Phase1 = DateTime.Now.AddHours(2).ToString("dd.MM.yy HH:mm");
            updateContestDTO.Phase2 = DateTime.Now.AddHours(30).ToString("dd.MM.yy HH:mm");
            updateContestDTO.Finished = DateTime.Now.AddHours(32).ToString("dd.MM.yy HH:mm");

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>().Object;
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;


            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
                categoryService.Setup(c => c.FindCategoryByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(arrContext.Categories.First()));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService.Object, userManager, signManager);
                var result = await sut.UpdateAsync(actContext.Contests.First().Name,updateContestDTO);

                Assert.AreEqual(updateContestDTO.Name, result.Name);
                Assert.AreEqual(updateContestDTO.Phase1, result.Phase1);
                Assert.AreEqual(updateContestDTO.Phase2, result.Phase2);
                Assert.AreEqual(updateContestDTO.Finished, result.Finished);
                Assert.AreEqual(updateContestDTO.CategoryName, result.Category);
            }
        }

        [TestMethod]
        public async Task ThrowWhen_Phase1Date_IsWrong()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_Phase1Date_IsWrong));
            var updateContestDTO = new Mock<UpdateContestDTO>().Object;
            updateContestDTO.Name = "UpdateTest";
            updateContestDTO.CategoryName = "Cars";
            updateContestDTO.IsOpen = true;
            updateContestDTO.Phase1 = "01.06.21 20:00";
            updateContestDTO.Phase2 = DateTime.Now.AddHours(30).ToString("dd.MM.yy HH:mm");
            updateContestDTO.Finished = DateTime.Now.AddHours(32).ToString("dd.MM.yy HH:mm");

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>().Object;
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
                categoryService.Setup(c => c.FindCategoryByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(arrContext.Categories.First()));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService.Object, userManager, signManager);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.UpdateAsync(actContext.Contests.First().Name, updateContestDTO));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_Phase2Date_IsWrong()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_Phase2Date_IsWrong));
            var updateContestDTO = new Mock<UpdateContestDTO>().Object;
            updateContestDTO.Name = "UpdateTest";
            updateContestDTO.CategoryName = "Cars";
            updateContestDTO.IsOpen = true;
            updateContestDTO.Phase1 = DateTime.Now.AddHours(2).ToString("dd.MM.yy HH:mm");
            updateContestDTO.Phase2 = "02.06.21 20:00";
            updateContestDTO.Finished = DateTime.Now.AddHours(32).ToString("dd.MM.yy HH:mm");

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>().Object;
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;


            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
                categoryService.Setup(c => c.FindCategoryByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(arrContext.Categories.First()));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService.Object, userManager, signManager);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.UpdateAsync(actContext.Contests.First().Name, updateContestDTO));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_FinishedDate_IsWrong()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_FinishedDate_IsWrong));
            var updateContestDTO = new Mock<UpdateContestDTO>().Object;
            updateContestDTO.Name = "UpdateTest";
            updateContestDTO.CategoryName = "Cars";
            updateContestDTO.IsOpen = true;
            updateContestDTO.Phase1 = DateTime.Now.AddHours(2).ToString("dd.MM.yy HH:mm");
            updateContestDTO.Phase2 = DateTime.Now.AddHours(30).ToString("dd.MM.yy HH:mm");
            updateContestDTO.Finished = "01.06.21 10:00";

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>().Object;
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
                categoryService.Setup(c => c.FindCategoryByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(arrContext.Categories.First()));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService.Object, userManager, signManager);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.UpdateAsync(actContext.Contests.First().Name, updateContestDTO));
            }
        }
    }
}

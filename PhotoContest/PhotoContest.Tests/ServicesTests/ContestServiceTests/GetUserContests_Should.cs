using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
using Microsoft.EntityFrameworkCore;

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class GetUserContests_Should
    {
        [TestMethod]
        public async Task Return_UserContests()
        {
            var options = Utils.GetOptions(nameof(Return_UserContests));

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
                var userContests = actContext.UserContests.Include(uc=>uc.Contest).Where(uc => uc.UserId == actContext.Users.Skip(2).First().Id);
                var contests = userContests.Select(uc => uc.Contest);
                var contestService = new ContestService(actContext, userService.Object, categoryService, userManager.Object, signManager);
                var sut = new ContestService(actContext, userService.Object, categoryService, userManager.Object, signManager);
                var result = await sut.GetUserContestsAsync();

                Assert.AreEqual(result.Count(), userContests.Count());
                Assert.AreEqual(string.Join(",", contests.Select(c => new ContestDTO(c))), string.Join(",", result));
            }
        }
    }
}

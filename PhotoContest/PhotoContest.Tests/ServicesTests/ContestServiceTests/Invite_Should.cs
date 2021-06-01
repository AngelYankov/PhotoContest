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
    public class Invite_Should
    {
        [TestMethod]
        public async Task ReturnTrue_IfUser_InvitedSuccessfully()
        {

            var options = Utils.GetOptions(nameof(ReturnTrue_IfUser_InvitedSuccessfully));

            var categoryService = new Mock<ICategoryService>();
            var userService = new Mock<IUserService>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var user = new Mock<User>();
            user.SetupGet(u => u.UserName).Returns("testUser@mail.com");

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                //await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();
                arrContext.Contests.First().IsOpen = false;
                await arrContext.SaveChangesAsync();

                userService.Setup(u => u.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(arrContext.Users.Skip(2).First()));
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var sut = new ContestService(actContext, contextAccessor.Object, userService.Object, categoryService.Object);
                var result = await sut.InviteAsync(actContext.Contests.First().Name, actContext.Users.Skip(2).First().UserName);

                Assert.IsTrue(result);
            }
        }
    }
}

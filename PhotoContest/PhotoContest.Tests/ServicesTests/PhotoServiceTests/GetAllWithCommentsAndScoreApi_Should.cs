using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

namespace PhotoContest.Tests.ServicesTests.PhotoServiceTests
{
    [TestClass]
    public class GetAllWithCommentsAndScoreApi_Should
    {
        [TestMethod]
        public async Task GetAllPhotosWithInfoApi()
        {
            var options = Utils.GetOptions(nameof(GetAllPhotosWithInfoApi));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var context = new Mock<HttpContext>();
            contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();

            var userContestService = new Mock<IUserContestService>();

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Reviews.AddRangeAsync(Utils.SeedReviews());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var userToGet = await actContext.Users.Include(u => u.Rank).Skip(3).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
                var contest = await actContext.Contests.Include(c => c.Status).Include(c => c.Category).FirstOrDefaultAsync(c => c.Name == "Best look");
                contestService.Setup(x => x.FindContestByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(contest));
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                var result = await sut.GetAllWithCommentsAndScoreApiAsync("Best look");
                var photos = await actContext.Photos
                                             .Include(p => p.User)
                                             .Include(p => p.Contest)
                                                .ThenInclude(c => c.Category)
                                             .ToListAsync();
                Assert.AreEqual(photos.Count(), result.Count());
            }
        }

        [TestMethod]
        public async Task Throw_When_UserIsNotInContestApi()
        {
            var options = Utils.GetOptions(nameof(Throw_When_UserIsNotInContestApi));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var context = new Mock<HttpContext>();
            contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();

            var userContestService = new Mock<IUserContestService>();

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Reviews.AddRangeAsync(Utils.SeedReviews());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var userToGet = await actContext.Users.Include(u => u.Rank).Skip(3).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
                var contest = await actContext.Contests.Include(c => c.Status).Include(c => c.Category).FirstOrDefaultAsync(c => c.Name == "Wild cats");
                contestService.Setup(x => x.FindContestByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(contest));
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetAllWithCommentsAndScoreApiAsync("Wild cats"));
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Services;
using System;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.PhotoServiceTests
{
    [TestClass]
    public class FindPhoto_Should
    {
        [TestMethod]
        public async Task ReturnPhotoWithCertainId()
        {
            var options = Utils.GetOptions(nameof(ReturnPhotoWithCertainId));

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
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                var result = await sut.FindPhotoAsync(Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                var photo = await actContext.Photos.FirstOrDefaultAsync(p => p.Id == Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                Assert.AreEqual(result.Title, photo.Title);
                Assert.AreEqual(result.Description, photo.Description);
                Assert.AreEqual(result.PhotoUrl, photo.PhotoUrl);
            }
        }
        [TestMethod]
        public async Task Throw_When_Photo_With_WrongID()
        {
            var options = Utils.GetOptions(nameof(Throw_When_Photo_With_WrongID));

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
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.FindPhotoAsync(Guid.Parse("3abe3a5d-e740-4899-a05e-d72bb57e727e")));
            }
        }
    }
}

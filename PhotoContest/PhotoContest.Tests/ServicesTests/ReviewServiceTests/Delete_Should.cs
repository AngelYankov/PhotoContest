using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ReviewServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public async Task DeleteReviewSuccessfully()
        {
            var options = Utils.GetOptions(nameof(DeleteReviewSuccessfully));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var context = new Mock<HttpContext>();
            contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);
            var userService = new Mock<IUserService>();
            var photoService = new Mock<IPhotoService>();

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Reviews.AddRangeAsync(Utils.SeedReviews());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);

                var result = await sut.DeleteAsync(Guid.Parse("8198e13a-30cb-4f4b-99f0-acf31a70b02d"));
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task Throw_When_ReviewID_IsNotValid()
        {
            var options = Utils.GetOptions(nameof(Throw_When_ReviewID_IsNotValid));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var context = new Mock<HttpContext>();
            contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);
            var userService = new Mock<IUserService>();
            var photoService = new Mock<IPhotoService>();

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);

                await Assert.ThrowsExceptionAsync<ArgumentException>(()=> sut.DeleteAsync(Guid.Parse("8198e13a-30cb-4f4b-99f0-acf31a70b02d")));
            }
        }

    }
}

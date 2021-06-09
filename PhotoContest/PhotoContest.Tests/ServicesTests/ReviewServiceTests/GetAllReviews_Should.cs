using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ReviewServiceTests
{
    [TestClass]
    public class GetAllReviews_Should
    {
        [TestMethod]
        public async Task GetAllReviewsBaseEntities()
        {
            var options = Utils.GetOptions(nameof(GetAllReviewsBaseEntities));
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
                await arrContext.Reviews.AddRangeAsync(Utils.SeedReviews());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                var result = await sut.GetAllReviewsAsync();
                var reviews = await actContext.Reviews.ToListAsync();
                Assert.AreEqual(reviews.Count(), result.Count());
            }
        }
    }
}

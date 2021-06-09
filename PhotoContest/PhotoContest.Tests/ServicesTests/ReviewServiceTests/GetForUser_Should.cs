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
    public class GetForUser_Should
    {
        [TestMethod]
        public async Task GetReviewForSpecificUser()
        {
            var options = Utils.GetOptions(nameof(GetReviewForSpecificUser));
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
                var userToGet = await actContext.Users.Skip(2).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));

                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                var result = await sut.GetForUserAsync(userToGet.UserName);
                var reviews = await actContext.Reviews.Include(r => r.Photo).Include(r => r.Evaluator).Where(r=>r.Photo.UserId == userToGet.Id).ToListAsync();
                Assert.AreEqual(result.Count(), reviews.Count());
            }
        }
    }
}

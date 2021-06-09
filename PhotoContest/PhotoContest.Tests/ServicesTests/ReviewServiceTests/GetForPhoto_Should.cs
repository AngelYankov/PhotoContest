using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ReviewServiceTests
{
    [TestClass]
    public class GetForPhoto_Should
    {
        [TestMethod]
        public async Task GetReviewsForPhoto()
        {
            var options = Utils.GetOptions(nameof(GetReviewsForPhoto));
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
                var photo = await actContext.Photos
                                            .Include(p => p.User)
                                            .FirstOrDefaultAsync(p => p.Id == Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                photoService.Setup(x => x.FindPhotoAsync(It.IsAny<Guid>())).Returns(Task.FromResult(photo));
                var reviews = await actContext.Reviews
                                        .Include(r => r.Photo)
                                        .Include(r => r.Evaluator)
                                        .Where(r => r.PhotoId == photo.Id && r.IsDeleted == false)
                                        .Select(r => new ReviewDTO(r))
                                        .ToListAsync();

                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                var result = await sut.GetForPhotoAsync(Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));

                Assert.AreEqual(result.Count(), reviews.Count());
                Assert.IsInstanceOfType(result, typeof(List<ReviewDTO>));
            }
        }
    }
}

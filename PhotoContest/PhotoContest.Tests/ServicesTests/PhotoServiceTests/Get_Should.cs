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
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.PhotoServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public async Task ReturnPhotoSuccesfully()
        {
            var options = Utils.GetOptions(nameof(ReturnPhotoSuccesfully));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();
            var userContestService = new Mock<IUserContestService>();

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                var photo = await actContext.Photos
                    .Include(p=>p.User)
                    .Include(p => p.Contest)
                         .ThenInclude(c => c.Category)
                    .Include(p => p.Contest)
                         .ThenInclude(c => c.Status)
                    .Where(p => p.IsDeleted == false)
                    .FirstOrDefaultAsync(p => p.Id == Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));

                var photoDTO = new PhotoDTO(photo);
                var result = await sut.GetAsync(Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                Assert.AreEqual(photoDTO.Title, result.Title);
                Assert.AreEqual(photoDTO.Description, result.Description);
                Assert.AreEqual(photoDTO.Contest, result.Contest);
                Assert.AreEqual(photoDTO.Category, result.Category);
            }
        }
        [TestMethod]
        public async Task Throw_When_InvalidIdOfPhoto()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidIdOfPhoto));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();
            var userContestService = new Mock<IUserContestService>();

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetAsync(Guid.Parse("3c1996fe-f204-42b7-9ea9-03a7196968e8")));
            }
        }
    }
}

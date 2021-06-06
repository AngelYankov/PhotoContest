using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.PhotoServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public async Task UpdatePhotoSuccesfully()
        {
            var options = Utils.GetOptions(nameof(UpdatePhotoSuccesfully));

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

            var updatePhotoDTO = new Mock<UpdatePhotoDTO>().Object;
            updatePhotoDTO.Title = "New photo";
            updatePhotoDTO.Description = "New description";
            updatePhotoDTO.PhotoUrl = "www.newphoto.com";

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                var result = await sut.UpdateAsync(updatePhotoDTO, Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                Assert.AreEqual(result.Title, updatePhotoDTO.Title);
                Assert.AreEqual(result.Description, updatePhotoDTO.Description);
                Assert.AreEqual(result.PhotoUrl, updatePhotoDTO.PhotoUrl);
                Assert.IsInstanceOfType(result, typeof(PhotoDTO));
            }
        }

        [TestMethod]
        public async Task UpdateTitleOnly()
        {
            var options = Utils.GetOptions(nameof(UpdateTitleOnly));

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

            var updatePhotoDTO = new Mock<UpdatePhotoDTO>().Object;
            updatePhotoDTO.Title = "New photo";
            updatePhotoDTO.Description = null;
            updatePhotoDTO.PhotoUrl = null;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                var result = await sut.UpdateAsync(updatePhotoDTO, Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                Assert.AreEqual(result.Title, updatePhotoDTO.Title);
                Assert.AreEqual(result.Description, "Picture of a lion.");
                Assert.AreEqual(result.PhotoUrl, "/Images/lion.jpg");
                Assert.IsInstanceOfType(result, typeof(PhotoDTO));
            }
        }

        [TestMethod]
        public async Task UpdateDescriptionOnly()
        {
            var options = Utils.GetOptions(nameof(UpdateDescriptionOnly));

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

            var updatePhotoDTO = new Mock<UpdatePhotoDTO>().Object;
            updatePhotoDTO.Title = null;
            updatePhotoDTO.Description = "new description";
            updatePhotoDTO.PhotoUrl = null;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                var result = await sut.UpdateAsync(updatePhotoDTO, Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                Assert.AreEqual(result.Title, "Lion King");
                Assert.AreEqual(result.Description, updatePhotoDTO.Description);
                Assert.AreEqual(result.PhotoUrl, "/Images/lion.jpg");
                Assert.IsInstanceOfType(result, typeof(PhotoDTO));
            }
        }

        [TestMethod]
        public async Task UpdateUrlOnly()
        {
            var options = Utils.GetOptions(nameof(UpdateUrlOnly));

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

            var updatePhotoDTO = new Mock<UpdatePhotoDTO>().Object;
            updatePhotoDTO.Title = null;
            updatePhotoDTO.Description = null;
            updatePhotoDTO.PhotoUrl = "newurl.com";

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                var result = await sut.UpdateAsync(updatePhotoDTO, Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                Assert.AreEqual(result.Title, "Lion King");
                Assert.AreEqual(result.Description, "Picture of a lion.");
                Assert.AreEqual(result.PhotoUrl, updatePhotoDTO.PhotoUrl);
                Assert.IsInstanceOfType(result, typeof(PhotoDTO));
            }
        }


        [TestMethod]
        public async Task Throw_When_Id_IsNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_Id_IsNotFound));

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

            var updatePhotoDTO = new Mock<UpdatePhotoDTO>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.UpdateAsync(updatePhotoDTO, Guid.Parse("874e0dd0-a2b2-449a-827b-03160c29d427")));
            }
        }
    }
}

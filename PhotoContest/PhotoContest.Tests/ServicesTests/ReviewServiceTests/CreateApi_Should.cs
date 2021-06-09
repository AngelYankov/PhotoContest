using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ReviewServiceTests
{
    [TestClass]
    public class CreateApi_Should
    {
        [TestMethod]
        public async Task Create_Review_SuccessfullyApi()
        {
            var options = Utils.GetOptions(nameof(Create_Review_SuccessfullyApi));
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

            var newReviewDTO = new Mock<NewReviewDTO>().Object;
            newReviewDTO.PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683");
            newReviewDTO.Comment = "new comment";
            newReviewDTO.Score = 5;
            newReviewDTO.WrongCategory = false;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var userToGet = await actContext.Users.Include(u => u.Rank).Skip(1).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
                var photo = await actContext.Photos
                                            .Include(p => p.User)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(p => p.Category)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(c => c.Status)
                                            .Where(p => p.IsDeleted == false)
                                            .FirstOrDefaultAsync(p => p.Id == Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                photoService.Setup(x => x.FindPhotoAsync(It.IsAny<Guid>())).Returns(Task.FromResult(photo));

                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                var result = await sut.CreateApiAsync(newReviewDTO);

                Assert.AreEqual(newReviewDTO.Comment, result.Comment);
                Assert.AreEqual(newReviewDTO.Score, result.Score);
                Assert.AreEqual(userToGet.FirstName + " " + userToGet.LastName, result.Evaluator);
                Assert.AreEqual(photo.Title, result.PhotoTitle);

            }
        }

        [TestMethod]
        public async Task Create_Review_WithWrongCategoryApi()
        {
            var options = Utils.GetOptions(nameof(Create_Review_WithWrongCategoryApi));
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

            var newReviewDTO = new Mock<NewReviewDTO>().Object;
            newReviewDTO.PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683");
            newReviewDTO.Comment = "new comment";
            newReviewDTO.Score = 5;
            newReviewDTO.WrongCategory = true;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var userToGet = await actContext.Users.Include(u => u.Rank).Skip(1).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
                var photo = await actContext.Photos
                                            .Include(p => p.User)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(p => p.Category)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(c => c.Status)
                                            .Where(p => p.IsDeleted == false)
                                            .FirstOrDefaultAsync(p => p.Id == Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                photoService.Setup(x => x.FindPhotoAsync(It.IsAny<Guid>())).Returns(Task.FromResult(photo));

                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                var result = await sut.CreateApiAsync(newReviewDTO);

                Assert.AreEqual("Photo is in a wrong category.", result.Comment);
                Assert.AreEqual(0, result.Score);
                Assert.AreEqual(userToGet.FirstName + " " + userToGet.LastName, result.Evaluator);
                Assert.AreEqual(photo.Title, result.PhotoTitle);
            }
        }

        [TestMethod]
        public async Task Throw_When_ContestIsInvalidPhaseApi()
        {
            var options = Utils.GetOptions(nameof(Throw_When_ContestIsInvalidPhaseApi));
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

            var newReviewDTO = new Mock<NewReviewDTO>().Object;
            newReviewDTO.PhotoId = Guid.Parse("94499cdd-e18c-4743-b0c4-2e1b7564c46c");
            newReviewDTO.Comment = "new comment";
            newReviewDTO.Score = 5;
            newReviewDTO.WrongCategory = false;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var userToGet = await actContext.Users.Include(u => u.Rank).Skip(1).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
                var photo = await actContext.Photos
                                            .Include(p => p.User)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(p => p.Category)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(c => c.Status)
                                            .Where(p => p.IsDeleted == false)
                                            .FirstOrDefaultAsync(p => p.Id == Guid.Parse("94499cdd-e18c-4743-b0c4-2e1b7564c46c"));
                photoService.Setup(x => x.FindPhotoAsync(It.IsAny<Guid>())).Returns(Task.FromResult(photo));

                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateApiAsync(newReviewDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_CommentIsNotValidApi()
        {
            var options = Utils.GetOptions(nameof(Throw_When_CommentIsNotValidApi));
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

            var newReviewDTO = new Mock<NewReviewDTO>().Object;
            newReviewDTO.PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683");
            newReviewDTO.Comment = null;
            newReviewDTO.Score = 5;
            newReviewDTO.WrongCategory = false;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var userToGet = await actContext.Users.Include(u => u.Rank).Skip(1).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
                var photo = await actContext.Photos
                                            .Include(p => p.User)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(p => p.Category)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(c => c.Status)
                                            .Where(p => p.IsDeleted == false)
                                            .FirstOrDefaultAsync(p => p.Id == Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                photoService.Setup(x => x.FindPhotoAsync(It.IsAny<Guid>())).Returns(Task.FromResult(photo));

                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateApiAsync(newReviewDTO));

            }
        }

        [TestMethod]
        public async Task Throw_When_UserIsNotInJuryApi()
        {
            var options = Utils.GetOptions(nameof(Throw_When_UserIsNotInJuryApi));
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

            var newReviewDTO = new Mock<NewReviewDTO>().Object;
            newReviewDTO.PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683");
            newReviewDTO.Comment = "new comment";
            newReviewDTO.Score = 5;
            newReviewDTO.WrongCategory = false;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var userToGet = await actContext.Users.Include(u => u.Rank).Skip(2).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
                var photo = await actContext.Photos
                                            .Include(p => p.User)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(p => p.Category)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(c => c.Status)
                                            .Where(p => p.IsDeleted == false)
                                            .FirstOrDefaultAsync(p => p.Id == Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                photoService.Setup(x => x.FindPhotoAsync(It.IsAny<Guid>())).Returns(Task.FromResult(photo));

                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateApiAsync(newReviewDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_PhotoIsAlreadyReviewedApi()
        {
            var options = Utils.GetOptions(nameof(Throw_When_PhotoIsAlreadyReviewedApi));
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

            var newReviewDTO = new Mock<NewReviewDTO>().Object;
            newReviewDTO.PhotoId = Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683");
            newReviewDTO.Comment = "new comment";
            newReviewDTO.Score = 5;
            newReviewDTO.WrongCategory = false;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var userToGet = await actContext.Users.Include(u => u.Rank).Skip(1).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
                var photo = await actContext.Photos
                                            .Include(p => p.User)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(p => p.Category)
                                            .Include(p => p.Contest)
                                                   .ThenInclude(c => c.Status)
                                            .Where(p => p.IsDeleted == false)
                                            .FirstOrDefaultAsync(p => p.Id == Guid.Parse("e165b91f-03bf-414e-88b7-c51b87775683"));
                photoService.Setup(x => x.FindPhotoAsync(It.IsAny<Guid>())).Returns(Task.FromResult(photo));

                var sut = new ReviewService(actContext, photoService.Object, userService.Object, contextAccessor.Object, userManager.Object, signManager);
                await sut.CreateAsync(newReviewDTO);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateApiAsync(newReviewDTO));

            }
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.PhotoServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public async Task CreatePhoto()
        {
            var options = Utils.GetOptions(nameof(CreatePhoto));

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

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = "New photo";
            newPhotoDTO.Description = "New description";
            newPhotoDTO.PhotoUrl = "www.newphoto.com";
            newPhotoDTO.ContestName = "Birds";

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
                var userToGet = await arrContext.Users.Skip(2).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var contest = await actContext.Contests.Include(c => c.Status).Include(c => c.Category).FirstOrDefaultAsync(c => c.Name == newPhotoDTO.ContestName);
                contestService.Setup(x => x.FindContestByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(contest));
                var userToGet = await actContext.Users.Skip(2).FirstAsync();
                var userContest = new Mock<UserContest>().Object;
                userContest.ContestId = contest.Id;
                userContest.UserId = userToGet.Id;
                await actContext.UserContests.AddAsync(userContest);
                await actContext.SaveChangesAsync();
                userContestService.Setup(x => x.GetAllUserContestsAsync()).Returns(Task.FromResult(actContext.UserContests.ToList()));
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                var result = await sut.CreateAsync(newPhotoDTO);

                Assert.AreEqual(result.Title, newPhotoDTO.Title);
                Assert.AreEqual(result.Description, newPhotoDTO.Description);
                Assert.AreEqual(result.PhotoUrl, newPhotoDTO.PhotoUrl);
                Assert.AreEqual(result.User, userToGet.FirstName + " " + userToGet.LastName);
                Assert.AreEqual(result.Username, userToGet.UserName);
                Assert.AreEqual(result.Contest, newPhotoDTO.ContestName);
                Assert.AreEqual(result.ContestStatus, contest.Status.Name);
                Assert.AreEqual(result.Category, contest.Category.Name);
                Assert.AreEqual(result.Points, 0);
                Assert.IsInstanceOfType(result, typeof(PhotoDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_TitleIsInvalid()
        {
            var options = Utils.GetOptions(nameof(Throw_When_TitleIsInvalid));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();
            var userContestService = new Mock<IUserContestService>();

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = null;

            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newPhotoDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_DescriptionIsInvalid()
        {
            var options = Utils.GetOptions(nameof(Throw_When_TitleIsInvalid));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();
            var userContestService = new Mock<IUserContestService>();

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = "New photo";
            newPhotoDTO.Description = null;

            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newPhotoDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_PhotoUrlIsInvalid()
        {
            var options = Utils.GetOptions(nameof(Throw_When_TitleIsInvalid));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();
            var userContestService = new Mock<IUserContestService>();

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = "New photo";
            newPhotoDTO.Description = "New photo descriptions";
            newPhotoDTO.PhotoUrl = null;

            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newPhotoDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_ContestNameIsInvalid()
        {
            var options = Utils.GetOptions(nameof(Throw_When_TitleIsInvalid));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();
            var userContestService = new Mock<IUserContestService>();

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = "New photo";
            newPhotoDTO.Description = "New photo descriptions";
            newPhotoDTO.PhotoUrl = "www.newphoto.com";
            newPhotoDTO.Description = null;

            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newPhotoDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_ContestIsNotInPhase1()
        {
            var options = Utils.GetOptions(nameof(Throw_When_ContestIsNotInPhase1));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var contestService = new Mock<IContestService>();
            var userService = new Mock<IUserService>();

            var userContestService = new Mock<IUserContestService>();

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = "New photo";
            newPhotoDTO.Description = "New description";
            newPhotoDTO.PhotoUrl = "www.newphoto.com";
            newPhotoDTO.ContestName = "Wild cats";

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var contest = await actContext.Contests.Include(c => c.Status).Include(c => c.Category).FirstOrDefaultAsync(c => c.Name == newPhotoDTO.ContestName);
                contestService.Setup(x => x.FindContestByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(contest));
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newPhotoDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_UserInJury()
        {
            var options = Utils.GetOptions(nameof(Throw_When_UserInJury));

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

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = "New photo";
            newPhotoDTO.Description = "New description";
            newPhotoDTO.PhotoUrl = "www.newphoto.com";
            newPhotoDTO.ContestName = "Birds";

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
                var userToGet = await arrContext.Users.FirstOrDefaultAsync(u => u.UserName == "kyle.sins@mail.com");
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var contest = await actContext.Contests.Include(c => c.Status).Include(c => c.Category).FirstOrDefaultAsync(c => c.Name == newPhotoDTO.ContestName);
                contestService.Setup(x => x.FindContestByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(contest));
                var userToGet = await actContext.Users.FirstOrDefaultAsync(u => u.UserName == "kyle.sins@mail.com");
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newPhotoDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_NotEnrolledInContest()
        {
            var options = Utils.GetOptions(nameof(Throw_When_NotEnrolledInContest));

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

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = "New photo";
            newPhotoDTO.Description = "New description";
            newPhotoDTO.PhotoUrl = "www.newphoto.com";
            newPhotoDTO.ContestName = "Birds";

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
                var userToGet = await arrContext.Users.Skip(2).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var contest = await actContext.Contests.Include(c => c.Status).Include(c => c.Category).FirstOrDefaultAsync(c => c.Name == newPhotoDTO.ContestName);
                contestService.Setup(x => x.FindContestByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(contest));
                var userToGet = await actContext.Users.Skip(2).FirstAsync();
                userContestService.Setup(x => x.GetAllUserContestsAsync()).Returns(Task.FromResult(actContext.UserContests.ToList()));
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newPhotoDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_AlreadyUploadedPhoto()
        {
            var options = Utils.GetOptions(nameof(Throw_When_AlreadyUploadedPhoto));

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

            var newPhotoDTO = new Mock<NewPhotoDTO>().Object;
            newPhotoDTO.Title = "New photo";
            newPhotoDTO.Description = "New description";
            newPhotoDTO.PhotoUrl = "www.newphoto.com";
            newPhotoDTO.ContestName = "Birds";

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
                var userToGet = await arrContext.Users.Skip(2).FirstAsync();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
                userService.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(userToGet));
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var contest = await actContext.Contests.Include(c => c.Status).Include(c => c.Category).FirstOrDefaultAsync(c => c.Name == newPhotoDTO.ContestName);
                contestService.Setup(x => x.FindContestByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(contest));
                var userToGet = await actContext.Users.Skip(2).FirstAsync();
                var userContest = new UserContest()
                {
                    ContestId = contest.Id,
                    UserId = userToGet.Id,
                    HasUploadedPhoto=true
                };
                await actContext.UserContests.AddAsync(userContest);
                await actContext.SaveChangesAsync();
                userContestService.Setup(x => x.GetAllUserContestsAsync()).Returns(Task.FromResult(actContext.UserContests.ToList()));
                var sut = new PhotoService(actContext, contextAccessor.Object, contestService.Object, userService.Object, userManager.Object, signManager, userContestService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newPhotoDTO));
            }
        }
    }
}

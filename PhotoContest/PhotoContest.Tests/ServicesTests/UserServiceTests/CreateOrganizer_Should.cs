using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserServiceTests
{
    [TestClass]
    public class CreateOrganizer_Should

    {
        [TestMethod]
        public async Task CreateNewOrganizer()
        {
            var options = Utils.GetOptions(nameof(CreateNewOrganizer));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.FirstName = "John";
            newUserDTO.LastName = "Smith";
            newUserDTO.Email = "john@mail.com";
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object,signManager);
                var result = await sut.CreateOrganizerAsync(newUserDTO);
                Assert.AreEqual(result.FirstName, newUserDTO.FirstName);
                Assert.AreEqual(result.LastName, newUserDTO.LastName);
                Assert.AreEqual(result.Email, newUserDTO.Email);
                Assert.AreEqual(result.Username, newUserDTO.Email);
                Assert.AreEqual(result.Rank, "Organizer");
                Assert.AreEqual(result.Points, 0);
                Assert.IsInstanceOfType(result, typeof(UserDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_InvalidFirstNameForOrganizer()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidFirstNameForOrganizer));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.LastName = "Smith";
            newUserDTO.Email = "John@mail.com";
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object,signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateOrganizerAsync(newUserDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_InvalidLastNameForOrganizer()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidLastNameForOrganizer));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.FirstName = "John";
            newUserDTO.Email = "John@mail.com";
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object,signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateOrganizerAsync(newUserDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_InvalidEmailForOrganizer()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidEmailForOrganizer));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.FirstName = "John";
            newUserDTO.LastName = "Smith";
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object,signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateOrganizerAsync(newUserDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_ExistingEmailWithDeletedAccountForOrganizer()
        {
            var options = Utils.GetOptions(nameof(Throw_When_ExistingEmailWithDeletedAccountForOrganizer));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.FirstName = "John";
            newUserDTO.LastName = "Smith";
            newUserDTO.Email = "john@mail.com";
            var user = new Mock<User>().Object;
            user.FirstName = "John";
            user.LastName = "Smith";
            user.Email = "john@mail.com";
            user.IsDeleted = true;

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Users.AddAsync(user);
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object,signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateOrganizerAsync(newUserDTO));
            }
        }

        [TestMethod]
        public async Task Throw_When_ExistingEmailForOrganizer()
        {
            var options = Utils.GetOptions(nameof(Throw_When_ExistingEmailForOrganizer));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.FirstName = "John";
            newUserDTO.LastName = "Smith";
            newUserDTO.Email = "john@mail.com";
            var user = new Mock<User>().Object;
            user.FirstName = "John";
            user.LastName = "Smith";
            user.Email = "john@mail.com";
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Users.AddAsync(user);
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object,signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateOrganizerAsync(newUserDTO));
            }
        }
    }
}

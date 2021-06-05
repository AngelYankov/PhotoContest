using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public async Task CreateNewUser()
        {
            var options = Utils.GetOptions(nameof(CreateNewUser));

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
            newUserDTO.Email = "john.smith@mail.com";
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object,signManager);
                var result = await sut.CreateAsync(newUserDTO);
                Assert.AreEqual(result.FirstName, newUserDTO.FirstName);
                Assert.AreEqual(result.LastName, newUserDTO.LastName);
                Assert.AreEqual(result.Email, newUserDTO.Email);
                Assert.AreEqual(result.Username, newUserDTO.Email);
                Assert.AreEqual(result.Rank, "Junkie");
                Assert.AreEqual(result.Points, 0);
                Assert.IsInstanceOfType(result, typeof(UserDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_InvalidFirstName()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidFirstName));

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
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newUserDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_InvalidLastName()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidLastName));

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
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newUserDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_InvalidEmail()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidEmail));

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
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newUserDTO));
            }
        }
        [TestMethod]
        public async Task Throw_When_ExistingEmailWithDeletedAccount()
        {
            var options = Utils.GetOptions(nameof(Throw_When_ExistingEmailWithDeletedAccount));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.FirstName = "John";
            newUserDTO.LastName = "Smith";
            newUserDTO.Email = "john.smith@mail.com";
            var user = new Mock<User>().Object;
            user.FirstName = "John";
            user.LastName = "Smith";
            user.Email = "john.smith@mail.com";
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
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newUserDTO));
            }
        }
        [TestMethod] //TODO
        public async Task Throw_When_IncorrectPassword()
        {
            var options = Utils.GetOptions(nameof(Throw_When_IncorrectPassword));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.FirstName = "John";
            newUserDTO.LastName = "Smith";
            newUserDTO.Email = "john.smith@mail.com";
            newUserDTO.Password = "123456";
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), newUserDTO.Password))
                .Returns(Task.FromResult(IdentityResult.Success)); //CHANGE TO NOT SUCCESS

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object,signManager);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newUserDTO));

            }
        }

        [TestMethod]
        public async Task Throw_When_ExistingEmail()
        {
            var options = Utils.GetOptions(nameof(Throw_When_ExistingEmail));

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
            newUserDTO.Email = "john.smith@mail.com";
            var user = new Mock<User>().Object;
            user.FirstName = "John";
            user.LastName = "Smith";
            user.Email = "john.smith@mail.com";
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
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateAsync(newUserDTO));
            }
        }
    }
}


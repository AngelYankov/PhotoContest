using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public async Task UpdateDetailsOfUser()
        {
            var options = Utils.GetOptions(nameof(UpdateDetailsOfUser));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var updateDTO = new UpdateUserDTO()
            {
                FirstName = "John",
                LastName = "Smith",
            };

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager,signManager);
                var userToUpdate = actContext.Users.Last();
                var result = await sut.UpdateAsync(updateDTO,userToUpdate.UserName);
                Assert.AreEqual(updateDTO.FirstName, result.FirstName);
                Assert.AreEqual(updateDTO.LastName, result.LastName);
                Assert.IsInstanceOfType(result, typeof(UserDTO));
            }
        }
        [TestMethod]
        public async Task KeepSameFirstName()
        {
            var options = Utils.GetOptions(nameof(KeepSameFirstName));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            var updateDTO = new UpdateUserDTO() {LastName="Smith"};

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager,signManager);
                var userToUpdate = actContext.Users.Last();
                var result = await sut.UpdateAsync(updateDTO, userToUpdate.UserName);
                var firstNameChecker = userToUpdate.FirstName;
                Assert.AreEqual(firstNameChecker, result.FirstName);
                Assert.AreEqual(updateDTO.LastName, result.LastName);
            }
        }
        [TestMethod]
        public async Task KeepSameSecondName()
        {
            var options = Utils.GetOptions(nameof(KeepSameSecondName));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;
            var updateDTO = new UpdateUserDTO() { FirstName="John"};

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager,signManager);
                var userToUpdate = actContext.Users.Last();
                var result = await sut.UpdateAsync(updateDTO, userToUpdate.UserName);
                var lastNameChecker = userToUpdate.LastName;
                Assert.AreEqual(updateDTO.FirstName, result.FirstName);
                Assert.AreEqual(lastNameChecker, result.LastName);
            }
        }
    }
}

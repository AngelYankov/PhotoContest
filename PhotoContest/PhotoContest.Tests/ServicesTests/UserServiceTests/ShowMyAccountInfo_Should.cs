using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Models;
using PhotoContest.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserServiceTests
{
    [TestClass]
    public class ShowMyAccountInfo_Should
    {
        [TestMethod]
        public async Task ReturnInfoOfLoggedUser()
        {
            var options = Utils.GetOptions(nameof(ReturnInfoOfLoggedUser));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager.Object, contextAccessor.Object, userPrincipalFactory, null, null, null, null).Object;
            var context = new Mock<HttpContext>();
            contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
                var userToGet = arrContext.Users.Skip(2).First();
                userManager.Setup(x => x.GetUserName(signManager.Context.User)).Returns(userToGet.UserName);
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager.Object, signManager);
                var userToGet = actContext.Users.Skip(2).First();
                var result = await sut.ShowMyAccountInfo();
                Assert.AreEqual(result.Email, userToGet.Email);
                Assert.AreEqual(result.FirstName, userToGet.FirstName);
                Assert.AreEqual(result.LastName, userToGet.LastName);
                Assert.AreEqual(result.Rank, userToGet.Rank.Name);
                Assert.AreEqual(result.Username, userToGet.UserName);
                Assert.IsInstanceOfType(result, typeof(UserDTO));
            }

        }
    }
}

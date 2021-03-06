using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public async Task DeleteUser()
        {
            var options = Utils.GetOptions(nameof(DeleteUser));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager,signManager);
                var result = await sut.DeleteAsync(actContext.Users.First().UserName);
                Assert.IsTrue(result);
                Assert.IsTrue(actContext.Users.First().IsDeleted);
            }
        }
    }
}

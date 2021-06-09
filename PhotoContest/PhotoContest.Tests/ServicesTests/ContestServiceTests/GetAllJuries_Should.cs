using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class GetAllJuries_Should
    {
        [TestMethod]
        public async Task Return_AllJuries()
        {
            var options = Utils.GetOptions(nameof(Return_AllJuries));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Juries.AddRangeAsync(Utils.SeedJuries());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService, userManager, signManager);
                var result = await sut.GetAllJuriesAsync();

                Assert.AreEqual(actContext.Juries.Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Juries), string.Join(",", result));
            }
        }
    }
}

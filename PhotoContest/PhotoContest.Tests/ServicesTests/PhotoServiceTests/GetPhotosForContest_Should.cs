using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.PhotoServiceTests
{
    [TestClass]
    public class GetPhotosForContest_Should
    {
        [TestMethod]
        public async Task GetAllPhotosForCertainContest()
        {
            var options = Utils.GetOptions(nameof(GetAllPhotosForCertainContest));

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
                var result = await sut.GetPhotosForContestAsync("Wild cats");
                var photos = await actContext.Photos
                                        .Include(p => p.User)
                                        .Include(p => p.Contest)
                                          .ThenInclude(c => c.Category)
                                        .Where(p => p.IsDeleted == false && p.Contest.Name == "Wild cats")
                                        .Select(p => new PhotoDTO(p))
                                        .ToListAsync();
                Assert.AreEqual(photos.Count(), result.Count());
                Assert.IsInstanceOfType(result, typeof(List<PhotoDTO>));
            }
        }
    }
}

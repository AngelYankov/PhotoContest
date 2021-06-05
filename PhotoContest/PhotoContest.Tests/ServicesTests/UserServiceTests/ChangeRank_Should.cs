using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserServiceTests
{
    [TestClass]
    public class ChangeRank_Should
    {
        [TestMethod]
        public async Task ChangeRankToEnthusiast()
        {
            var options = Utils.GetOptions(nameof(ChangeRankToEnthusiast));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager,signManager);
                var userToChangeRank = actContext.Users.Include(u => u.Rank).Skip(2).First();
                userToChangeRank.OverallPoints += 60;
                await sut.ChangeRank();
                Assert.AreEqual(userToChangeRank.Rank.Name, "Enthusiast"); //DO NOT CHANGE RANK, ONLY RANKID
            }
        }
        [TestMethod]
        public async Task ChangeRankToMaster()
        {
            var options = Utils.GetOptions(nameof(ChangeRankToMaster));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager,signManager);
                var userToChangeRank = actContext.Users.Include(u => u.Rank).Skip(2).First();
                userToChangeRank.OverallPoints += 160;
                await sut.ChangeRank();
                Assert.AreEqual(userToChangeRank.Rank.Name, "Master"); //DO NOT CHANGE RANK, ONLY RANKID
            }
        }
        [TestMethod]
        public async Task ChangeRankToDictator()
        {
            var options = Utils.GetOptions(nameof(ChangeRankToDictator));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var signManager = new Mock<SignInManager<User>>(userManager, contextAccessor, userPrincipalFactory, null, null, null, null).Object;
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager,signManager);
                var userToChangeRank = actContext.Users.Include(u => u.Rank).Skip(2).First();
                userToChangeRank.OverallPoints += 1100;
                await sut.ChangeRank();
                Assert.AreEqual(userToChangeRank.Rank.Name, "Wise and Benevolent Photo Dictator"); //DO NOT CHANGE RANK, ONLY RANKID
            }
        }
    }
}

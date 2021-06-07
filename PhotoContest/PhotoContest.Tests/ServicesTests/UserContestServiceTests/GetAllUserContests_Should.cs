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

namespace PhotoContest.Tests.ServicesTests.UserContestServiceTests
{
    [TestClass]
    public class GetAllUserContests_Should
    {
        [TestMethod]
        public async Task ReturnAllUserContests()
        {
            var options = Utils.GetOptions(nameof(ReturnAllUserContests));
            var contestService = new Mock<IContestService>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserContestService(actContext, contestService);
                var result = await sut.GetAllUserContestsAsync();
                var userContest = await actContext.UserContests.ToListAsync();
                Assert.AreEqual(result.Count(), userContest.Count());
            }
        }
    }
}

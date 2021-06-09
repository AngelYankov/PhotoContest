using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserContestServiceTests
{
    [TestClass]
    public class CalculatePoints_Should
    {
        [TestMethod]
        public async Task CalculatePointsCorrectly()
        {
            var options = Utils.GetOptions(nameof(CalculatePointsCorrectly));
            var contestService = new Mock<IContestService>();

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.UserContests.AddRangeAsync(Utils.SeedUserContests());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.Reviews.AddRangeAsync(Utils.SeedReviews());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var finishedContests = await actContext.Contests.Where(c => c.Status.Name == "Finished" && c.IsCalculated == false).ToListAsync();
                var userJohn = await actContext.Users.FirstOrDefaultAsync(u => u.UserName == "john.smith@mail.com");
                var userKyle = await actContext.Users.FirstOrDefaultAsync(u => u.UserName == "kyle.sins@mail.com");
                contestService.Setup(x => x.GetAllFinishedContestsAsync()).Returns(Task.FromResult(finishedContests));
                var sut = new UserContestService(actContext, contestService.Object);
                await sut.CalculatePointsAsync();
                Assert.AreEqual(userKyle.OverallPoints, 1236);
                Assert.AreEqual(userJohn.OverallPoints, 76);
            }
        }
    }
}

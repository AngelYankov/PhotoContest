using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoContest.Data;
using PhotoContest.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.CategoryServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public async Task Return_AllCategoriesNames()
        {
            var options = Utils.GetOptions(nameof(Return_AllCategoriesNames));
            var categories = Utils.SeedCategories();
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Categories.AddRangeAsync(categories);
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new CategoryService(actContext);
                var result = await sut.GetAllAsync();
                Assert.AreEqual(categories.Count, result.ToList().Count);
                Assert.AreEqual(string.Join(",", categories.Select(c=>c.Name)), string.Join(",", result));
            }
        }
    }
}

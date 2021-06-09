using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.CategoryServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public async Task Return_UpdatedCategoryName()
        {
            var options = Utils.GetOptions(nameof(Return_UpdatedCategoryName));
            var category = new Mock<Category>().Object;
            category.Name = "Test category";
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Categories.AddAsync(category);
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options)) 
            {
                var sut = new CategoryService(actContext);
                var result = await sut.UpdateAsync(category.Name, "New name");
                Assert.AreEqual(actContext.Categories.First().Name, result);
            }
        }
    }
}

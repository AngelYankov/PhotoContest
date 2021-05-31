using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoContest.Data;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var categories = Utils.SeedCategories();
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Categories.AddRangeAsync(categories);
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options)) 
            {
                var sut = new CategoryService(actContext);
                var result = await sut.UpdateAsync(categories[0].Name, "New name");
                await actContext.SaveChangesAsync();
                Assert.AreEqual(categories[0].Name, result);
            }
        }
    }
}

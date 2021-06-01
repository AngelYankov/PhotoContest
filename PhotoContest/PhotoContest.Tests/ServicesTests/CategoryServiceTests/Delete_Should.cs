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
    public class Delete_Should
    {
        [TestMethod]
        public async Task Return_True_IfCategoryIsDeleted()
        {
            var options = Utils.GetOptions(nameof(Return_True_IfCategoryIsDeleted));
            var categories = Utils.SeedCategories();
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Categories.AddRangeAsync(categories);
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new CategoryService(actContext);
                var result = await sut.DeleteAsync(categories[0].Name);
                Assert.IsTrue(result);
                Assert.AreEqual(actContext.Categories.Where(c => c.IsDeleted == false).Count(), 4);
            }
        }
    }
}

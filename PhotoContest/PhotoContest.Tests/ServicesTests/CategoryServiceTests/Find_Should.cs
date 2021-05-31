using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoContest.Data;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.CategoryServiceTests
{
    [TestClass]
    public class Find_Should
    {
        [TestMethod]
        public async Task Return_NameOf_CategoryAsync()
        {
            var options = Utils.GetOptions(nameof(Return_NameOf_CategoryAsync));
            var categories = Utils.SeedCategories();
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.AddRangeAsync(categories);
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new CategoryService(actContext);
                var result = await sut.FindCategoryByNameAsync(categories[0].Name);
                Assert.AreEqual(categories[0].Id, result.Id);
                Assert.AreEqual(categories[0].Name, result.Name);
            }
        }

        [TestMethod]
        public async Task ThrowsWhen_Category_IsDeleted()
        {
            var options = Utils.GetOptions(nameof(ThrowsWhen_Category_IsDeleted));
            var categories = Utils.SeedCategories();
            categories[0].IsDeleted = true;
            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.AddRangeAsync(categories);
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new CategoryService(actContext);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.FindCategoryByNameAsync(categories[0].Name));
            }
        }

        [TestMethod]
        public async Task ThrowsWhen_Category_NotFound()
        {
            var options = Utils.GetOptions(nameof(ThrowsWhen_Category_NotFound));
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new CategoryService(actContext);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.FindCategoryByNameAsync("Wrong name"));
            }
        }
    }
}

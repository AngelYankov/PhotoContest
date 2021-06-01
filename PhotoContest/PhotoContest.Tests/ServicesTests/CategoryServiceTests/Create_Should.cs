using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.CategoryServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public async Task Return_NameOf_CreatedCategoryAsync()
        {
            var options = Utils.GetOptions(nameof(Return_NameOf_CreatedCategoryAsync));
            var category = new Mock<Category>();
            category.Name = "Test category";
            using (var arrContext = new PhotoContestContext(options))
            {
                arrContext.Categories.Add(category.Object);
                arrContext.SaveChanges();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new CategoryService(actContext);
                var result = await sut.CreateAsync("Test category");
                Assert.AreEqual(result, category.Name);
                Assert.AreEqual(actContext.Categories.Count(), 2);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class GetByPhase_Should
    {
        [TestMethod]
        public async Task Return_Contests_InPhase1_NotSorted()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase1_NotSorted));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 1", null, null);

                Assert.AreEqual(actContext.Contests.Where(c=>c.Status.Name=="Phase 1").Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Contests
                                                           .Where(c => c.Status.Name == "Phase 1")
                                                           .Select(c => new ContestDTO(c))), string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase1_SortedByName()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase1_SortedByName));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 1", "name", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 1").Count(), result.Count());
                
                Assert.AreEqual(string.Join(",",actContext.Contests.Where(c => c.Status.Name == "Phase 1")
                                                                   .OrderBy(c => c.Name)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",",result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase1_SortedByName_Desc()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase1_SortedByName_Desc));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 1", "name", "desc");

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 1").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 1")
                                                                   .OrderByDescending(c => c.Name)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase1_SortedByCategory()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase1_SortedByCategory));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 1", "category", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 1").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 1")
                                                                   .OrderBy(c => c.Category)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase1_SortedByCategoryDesc()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase1_SortedByCategoryDesc));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 1", "category", "desc");

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 1").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 1")
                                                                   .OrderByDescending(c => c.Category)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase1_SortedByNewest()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase1_SortedByNewest));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 1", "newest", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 1").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 1")
                                                                   .OrderBy(c => c.Phase1)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase1_SortedByOldest()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase1_SortedByOldest));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 1", "oldest", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 1").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 1")
                                                                   .OrderByDescending(c => c.Finished)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }



        ////////Phase 2....................................................................................
        ///

        [TestMethod]
        public async Task Return_Contests_InPhase2_NotSorted()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase2_NotSorted));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 2", null, null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 2").Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Contests
                                                           .Where(c => c.Status.Name == "Phase 2")
                                                           .Select(c => new ContestDTO(c))), string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase2_SortedByName()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase2_SortedByName));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 2", "name", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 2").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 2")
                                                                   .OrderBy(c => c.Name)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase2_SortedByName_Desc()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase2_SortedByName_Desc));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 2", "name", "desc");

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 2").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 2")
                                                                   .OrderByDescending(c => c.Name)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase2_SortedByCategory()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase2_SortedByCategory));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 2", "category", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 2").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 2")
                                                                   .OrderBy(c => c.Category)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase2_SortedByCategoryDesc()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase2_SortedByCategoryDesc));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 2", "category", "desc");

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 2").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 2")
                                                                   .OrderByDescending(c => c.Category)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase2_SortedByNewest()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase2_SortedByNewest));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 2", "newest", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 2").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 2")
                                                                   .OrderBy(c => c.Phase1)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InPhase2_SortedByOldest()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InPhase2_SortedByOldest));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Phase 2", "oldest", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Phase 2").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Phase 2")
                                                                   .OrderByDescending(c => c.Finished)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }


        ////Finished.....................................................................
        ///
        [TestMethod]
        public async Task Return_Contests_InFinished_NotSorted()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InFinished_NotSorted));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Finished", null, null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Finished").Count(), result.Count());
                Assert.AreEqual(string.Join(",", actContext.Contests
                                                           .Where(c => c.Status.Name == "Finished")
                                                           .Select(c => new ContestDTO(c))), string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InFinished_SortedByName()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InFinished_SortedByName));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Finished", "name", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Finished").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Finished")
                                                                   .OrderBy(c => c.Name)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InFinished_SortedByName_Desc()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InFinished_SortedByName_Desc));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Finished", "name", "desc");

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Finished").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Finished")
                                                                   .OrderByDescending(c => c.Name)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InFinished_SortedByCategory()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InFinished_SortedByCategory));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Finished", "category", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Finished").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Finished")
                                                                   .OrderBy(c => c.Category)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InFinished_SortedByCategoryDesc()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InFinished_SortedByCategoryDesc));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Finished", "category", "desc");

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Finished").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Finished")
                                                                   .OrderByDescending(c => c.Category)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InFinished_SortedByNewest()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InFinished_SortedByNewest));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Finished", "newest", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Finished").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Finished")
                                                                   .OrderBy(c => c.Phase1)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task Return_Contests_InFinished_SortedByOldest()
        {
            var options = Utils.GetOptions(nameof(Return_Contests_InFinished_SortedByOldest));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);
                var result = await sut.GetByPhaseAsync("Finished", "oldest", null);

                Assert.AreEqual(actContext.Contests.Where(c => c.Status.Name == "Finished").Count(), result.Count());

                Assert.AreEqual(string.Join(",", actContext.Contests.Where(c => c.Status.Name == "Finished")
                                                                   .OrderByDescending(c => c.Finished)
                                                                   .Select(c => new ContestDTO(c))),
                                                                   string.Join(",", result));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_Phase_Incorrect()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_Phase_Incorrect));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetByPhaseAsync("Wrong", null, null));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_SortBy_Incorrect()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_SortBy_Incorrect));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetByPhaseAsync("Phase 1", "wrong", null));
            }
        }

        [TestMethod]
        public async Task ThrowWhen_OrderBy_Incorrect()
        {
            var options = Utils.GetOptions(nameof(ThrowWhen_SortBy_Incorrect));

            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Statuses.AddRangeAsync(Utils.SeedStatuses());
                await arrContext.Categories.AddRangeAsync(Utils.SeedCategories());
                await arrContext.Photos.AddRangeAsync(Utils.SeedPhotos());
                await arrContext.Contests.AddRangeAsync(Utils.SeedContests());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var contestService = new ContestService(actContext, contextAccessor, userService, categoryService);
                var sut = new ContestService(actContext, contextAccessor, userService, categoryService);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetByPhaseAsync("Phase 1", "asc", "wrong"));
            }
        }
    }
}

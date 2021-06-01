﻿using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Models;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public async Task ReturnAllUsers()
        {
            var options = Utils.GetOptions(nameof(ReturnAllUsers));
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;

            using (var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            };
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager);
                var result = await sut.GetAllAsync();
                Assert.AreEqual(result.Count(), actContext.Users.Count());
                Assert.AreEqual(string.Join(", ",result),string.Join(", ", actContext.Users.Select(u=>new UserDTO(u))));
            }
        }
    }
}

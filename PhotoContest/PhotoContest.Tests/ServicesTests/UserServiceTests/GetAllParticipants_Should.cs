using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class GetAllParticipants_Should
    {
        [TestMethod]
        public async Task ReturnAllParticipants()
        {
            var options = Utils.GetOptions(nameof(ReturnAllParticipants));

            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null,
                null, null, null, null, null).Object;
            using(var arrContext = new PhotoContestContext(options))
            {
                await arrContext.Ranks.AddRangeAsync(Utils.SeedRanks());
                await arrContext.Roles.AddRangeAsync(Utils.SeedRoles());
                await arrContext.UserRoles.AddRangeAsync(Utils.SeedUserRoles());
                await arrContext.Users.AddRangeAsync(Utils.SeedUsers());
                await arrContext.SaveChangesAsync();
            }
            using (var actContext = new PhotoContestContext(options))
            {
                var sut = new UserService(actContext, userManager);
                var result = await sut.GetAllParticipantsAsync();
                var role = await actContext.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == "user");
                var userRoles = await actContext.UserRoles.Where(ur => ur.RoleId == role.Id).ToListAsync();
                var filteredUsers = new List<User>();
                foreach (var userRole in userRoles)
                {
                    var user = await actContext.Users.Include(u => u.Rank).FirstOrDefaultAsync(u => u.Id == userRole.UserId);
                    filteredUsers.Add(user);
                }
                var output = filteredUsers.Select(u => new UserDTO(u)).OrderByDescending(u => u.Points);
                Assert.AreEqual(output.Count(), result.Count());
                Assert.AreEqual(string.Join(", ", output), string.Join(", ", result));
            }
        }
    }
}

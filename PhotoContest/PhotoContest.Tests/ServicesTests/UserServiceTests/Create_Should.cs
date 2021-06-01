using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Tests.ServicesTests.UserServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public async Task CreateNewUser()
        {
            /*var options = Utils.GetOptions(nameof(CreateNewUser));
            var newUserDTO = new Mock<NewUserDTO>().Object;
            newUserDTO.FirstName = "John";
            newUserDTO.LastName = "Smith";
            newUserDTO.Email = "john.smith@mail.com";
            using (var actContext = new PhotoContestContext(options))
            {
                var userManager = new Mock<UserManager<User>>().Object;
                var sut = new UserService(actContext, userManager);
                var result = await sut.CreateAsync(newUserDTO);
                Assert.AreEqual(result.FirstName, newUserDTO.FirstName);
                Assert.AreEqual(result.LastName, newUserDTO.LastName);
                Assert.AreEqual(result.Email, newUserDTO.Email);
                Assert.AreEqual(result.Username, newUserDTO.Email);
                Assert.AreEqual(result.Rank, "Junkie");
                Assert.AreEqual(result.Points, 0);
                Assert.IsInstanceOfType(result, typeof(UserDTO));
            }*/
        }
    }
}

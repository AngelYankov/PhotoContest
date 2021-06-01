using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Services.Models.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Tests.ServicesTests.ContestServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Return_Created_Contest()
        {
            var options = Utils.GetOptions(nameof(Return_Created_Contest));
            var newContestDTO = new Mock<NewContestDTO>();
        }
    }
}

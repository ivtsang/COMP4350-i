using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectR.Controllers;
using ConnectR.Models;
using ConnectR.Repositories;

namespace ConnectR.Tests.Controllers
{
    [TestClass]
    public class ConferenceControllerTest
    {
        [TestMethod]
        public void Create()
        {
            ConferenceRepository repo = new ConferenceRepository();

            ConferenceModel conference = new ConferenceModel
            {
                ProfileId = 13,
                Location = "TestLocation",
                Date = new DateTime(2016, 1, 1),
                Content = "TestContet",
                Title = "TestTitle"
            };

            repo.SaveConference(conference);

            var result = repo.SearchConference(conference);
            Assert.IsTrue(result.Count() > 0);
        }
    }
}

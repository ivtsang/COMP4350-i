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
                ProfileId = 1,
                Location = "TestLocation",
                Date = new DateTime(2016, 1, 1),
                Content = "TestContet",
                Title = "TestTitle"
            };

            repo.SaveConference(conference);

            var result = repo.SearchConference(conference);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void Update()
        {
            ConferenceRepository repo = new ConferenceRepository();

            Conference result = new Conference();
            ConferenceModel updated;
            DateTime newDate = new DateTime(2016, 2, 2);

            ConferenceModel conference = new ConferenceModel
            {
                ProfileId = 1,
                Location = "TestLocation",
                Date = new DateTime(2016, 1, 1),
                Content = "TestContet",
                Title = "TestTitle"
            };

            result = repo.SearchConference(conference).FirstOrDefault();

            if(result != null)
            {
                result.Location = "UpdateLocation";
                result.Content = "UpdateContent";
                result.Title = "UpdateTitle";
                result.Date = newDate;

                updated = repo.ConvertToModel(result);

                repo.UpdateConference(updated);

                result = repo.SearchConference(updated).FirstOrDefault();

                Assert.IsTrue(result.Location == "UpdateLocation");
                Assert.IsTrue(result.Content == "UpdateContent");
                Assert.IsTrue(result.Title == "UpdateTitle");
                Assert.IsTrue(result.Date == newDate);
            }
        }

        [TestMethod]
        public void Delete()
        {
            ConferenceRepository repo = new ConferenceRepository();

            List<Conference> result = new List<Conference>();

            ConferenceModel conference = new ConferenceModel
            {
                ProfileId = 1,
                Location = "UpdateLocation",
                Date = new DateTime(2016, 2, 2),
                Content = "UpdateContent",
                Title = "UpdateTitle"
            };

            result = repo.SearchConference(conference);
            if(result != null)
            {
                Assert.IsTrue(repo.DeleteConference(result.FirstOrDefault().ConferenceId) == 1);
            }
        }
    }
}

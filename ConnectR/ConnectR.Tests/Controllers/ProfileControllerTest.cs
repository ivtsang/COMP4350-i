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
    public class ProfileControllerTest
    {
        private ProfileRepository repo = new ProfileRepository();

        [ClassInitialize]
        public static void SetupClass(TestContext testContext)
        {
            ProfileRepository repo = new ProfileRepository();

            Profile profile = new Profile
            {
                UserId = "TestUserIdDelete",
            };

            repo.SaveProfile(profile);

            profile = new Profile
            {
                UserId = "TestUserIdSearch",
            };

            repo.SaveProfile(profile);
        }

        [ClassCleanup]
        public static void TeardownClass()
        {
            ProfileRepository repo = new ProfileRepository();
            var result = repo.GetProfileByUserId("TestUserId");

            if (result != null)
            {
                repo.DeleteProfile(result.ProfileId);
            }

            result = repo.GetProfileByUserId("TestUserId2");

            if (result != null)
            {
                repo.DeleteProfile(result.ProfileId);
            }

            result = repo.GetProfileByUserId("TestUserIdDelete");

            if (result != null)
            {
                repo.DeleteProfile(result.ProfileId);
            }

            result = repo.GetProfileByUserId("TestUserIdSearch");

            if (result != null)
            {
                repo.DeleteProfile(result.ProfileId);
            }

        }

        [TestMethod]
        public void Create()
        {
            Profile profile = new Profile
            {
                ProfileId = 10000,
                UserId = "TestUserId",
                FirstName = "testFN",
                LastName = "testLN",
                Country = "testCoun",
                City = "tedtCity",
                School = "testSchool",
                Degree = "testDeg",
                About = "testAbout"
            };

            repo.SaveProfile(profile);

            var result = repo.GetProfileByUserId("TestUserId");
            Assert.IsNotNull(result.ProfileId);
            Assert.AreEqual("TestUserId", result.UserId);
            Assert.AreEqual("testFN", result.FirstName);
            Assert.AreEqual("testLN", result.LastName);
            Assert.AreEqual("testCoun", result.Country);
            Assert.AreEqual("tedtCity", result.City);
            Assert.AreEqual("testSchool", result.School);
            Assert.AreEqual("testDeg", result.Degree);
            Assert.AreEqual("testAbout", result.About);
            Assert.IsNull(result.Age);
            Assert.IsNull(result.UserImage);
        }

        [TestMethod]
        public void Update()
        {
            Profile profile = new Profile
            {
                UserId = "TestUserId2",
            };

            profile = repo.SaveProfile(profile);

            if (profile != null)
            {
                Assert.IsNotNull(profile.ProfileId);
                Assert.AreEqual("TestUserId2", profile.UserId);
                Assert.IsNull(profile.FirstName);
                Assert.IsNull(profile.LastName);
                Assert.IsNull(profile.Country);
                Assert.IsNull(profile.City);
                Assert.IsNull(profile.School);
                Assert.IsNull(profile.Degree);
                Assert.IsNull(profile.About);
                Assert.IsNull(profile.Age);
                Assert.IsNull(profile.UserImage);

                profile.FirstName = "testFN2";
                profile.LastName = "testLN2";
                profile.Country = "testCoun2";
                profile.City = "tedtCity2";

                repo.UpdateProfile(profile);

                var result = repo.GetProfileByUserId("TestUserId2");

                Assert.IsNotNull(result.ProfileId);
                Assert.AreEqual("TestUserId2", result.UserId);
                Assert.AreEqual("testFN2", result.FirstName);
                Assert.AreEqual("testLN2", result.LastName);
                Assert.AreEqual("testCoun2", result.Country);
                Assert.AreEqual("tedtCity2", result.City);
                Assert.IsNull(result.School);
                Assert.IsNull(result.Degree);
                Assert.IsNull(result.About);
                Assert.IsNull(result.Age);
                Assert.IsNull(result.UserImage);
            }
        }

        [TestMethod]
        public void Delete()
        {
            var profile = repo.GetProfileByUserId("testUserIdDelete");

            Assert.IsNotNull(profile);

            repo.DeleteProfile(profile.ProfileId);

            profile = repo.GetProfileByUserId("testUserIdDelete");

            Assert.IsNull(profile);
        }

        [TestMethod]
        public void getAll()
        {
            Assert.IsTrue(repo.GetProfiles(0).Count() > 0);
        }

        [TestMethod]
        public void getById()
        {
            Assert.IsTrue(repo.GetProfileById(1).ProfileId == 1);
        }

        [TestMethod]
        public void getByUserId()
        {
            Assert.IsTrue(repo.GetProfileByUserId("TestUserIdSearch").UserId == "TestUserIdSearch");
            Assert.IsTrue(repo.GetProfileByUserId("") == null);
        }

    }
}

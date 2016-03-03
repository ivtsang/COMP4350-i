using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConnectR.Models;

namespace ConnectR.Repositories
{
    public class ProfileRepository
    {
        private Entities db = new Entities();

        public ProfileModel GetProfileByUserId(string userId)
        {
            var query = from p in db.Profiles
                        where p.UserId == userId
                        select p;
            Profile profile = query.ToList().First();

            return ConvertToModel(profile);
        }

        public ProfileModel ConvertToModel(Profile profile)
        {
            ProfileModel profileM = new ProfileModel()
            {
                ProfileId = profile.ProfileId,
                UserId = profile.UserId,
                Age = profile.Age,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Degree = profile.Degree,
                City = profile.City,
                Country = profile.Country,
                School = profile.School
            };

            return profileM;
        }
    }
}
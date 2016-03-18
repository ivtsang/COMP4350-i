using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConnectR.Models;
using System.Data.Entity;

namespace ConnectR.Repositories
{
    public class ProfileRepository
    {
        private Entities db = new Entities();

        public IEnumerable<ProfileModel> GetProfiles()
        {
            List<ProfileModel> profiles = new List<ProfileModel>();
            foreach (var p in db.Profiles)
            {
                profiles.Add(ConvertToModel(p));
            }
            return profiles;
        }

        public ProfileModel GetProfileById(int id)
        {
            Profile p = db.Profiles.Include("Files").SingleOrDefault(e => e.ProfileId == id);
            return ConvertToModel(p);
        }

        public Profile SaveProfile(Profile profile)
        {
            Profile newProfile = db.Profiles.Add(profile);
            db.SaveChanges();
            if(profile.Files.SingleOrDefault<File>() != null)
            {
                profile.UserImage = profile.Files.SingleOrDefault<File>().Id;
                db.SaveChanges();
            }
            return newProfile;
        }

        public void UpdateProfile(Profile profile)
        {
            File newImg;
            if (profile.UserImage > 0)
            {
                db.Files.Remove(profile.Files.First(f => f.Id == profile.UserImage));
            }
            newImg = profile.Files.FirstOrDefault();
            if(newImg != null)
            {
                newImg.ProfileId = profile.ProfileId;
                db.Files.Add(newImg);
                db.SaveChanges();
                profile.UserImage = newImg.Id;
            }
            db.Entry(profile).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteProfile(int id)
        {
            Profile profile = db.Profiles.Find(id);
            List<File> fileList = (from f in db.Files where f.ProfileId == profile.ProfileId select f).ToList<File>();
            foreach (File f in fileList)
            {
                db.Files.Remove(f);
            }
            db.Profiles.Remove(profile);
            db.SaveChanges();
        }

        public ProfileModel GetProfileByUserId(string userId)
        {
            var query = from p in db.Profiles
                        where p.UserId == userId
                        select p;
            Profile profile = query.ToList().SingleOrDefault();

            if (profile != null)
                return ConvertToModel(profile);
            else
                return null;
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
                School = profile.School,
                UserImage = profile.UserImage,
                About = profile.About,
            };

            return profileM;
        }
    }
}
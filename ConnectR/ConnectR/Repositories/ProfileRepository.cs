﻿using System;
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

        public IEnumerable<ProfileModel> GetProfiles(int id)
        {
            List<ProfileModel> profiles = new List<ProfileModel>();
            foreach (var p in db.Profiles)
            {
                profiles.Add(ConvertToModel(p));
            }

            foreach (ProfileModel p in profiles)
            {
                CheckIfFollowing(id, p);
            }
            return profiles;
        }

        public ProfileModel GetProfileById(int id)
        {
            Profile p = db.Profiles.Include("Files").SingleOrDefault(e => e.ProfileId == id);
            ProfileModel profileM = ConvertToModel(p);

            profileM.NumFollowers = GetFollowers(id).Count();
            profileM.NumFollowing = GetFollowing(id).Count();

            return profileM;
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

        public void CheckIfFollowing(int profileId, ProfileModel profile)
        {
            var result = (from f in db.Followers
                          where f.FollowerId == profileId
                          where f.FollowingId == profile.ProfileId
                          select f).FirstOrDefault();

            if(result != null)
            {
                profile.Followed = true;
            }
            else
            {
                profile.Followed = false;
            }
        }

        public void UnfollowProfile(int followerId, int followingId)
        {
            var result = (from f in db.Followers
                          where f.FollowerId == followerId
                          where f.FollowingId == followingId
                          select f).FirstOrDefault();
            if(result != null)
            {
                db.Followers.Remove(result);
                db.SaveChanges();
            }
            
        }

        public void FollowProfile(int followerId, int followingId)
        {
            Follower follower = new Follower { FollowerId = followerId, FollowingId = followingId };
            db.Followers.Add(follower);
            db.SaveChanges();
        }

        public IEnumerable<ProfileModel> GetFollowers(int id)
        {
            List<ProfileModel> profiles = new List<ProfileModel>();
            ProfileModel profileM;

            var result = from p in db.Profiles
                         join f in db.Followers on p.ProfileId equals f.FollowerId
                         where f.FollowingId == id
                         select p;

            foreach (var p in result)
            {
                profileM = ConvertToModel(p);
                CheckIfFollowing(id, profileM);
                profiles.Add(profileM);
            }


            return profiles;
        }

        public IEnumerable<ProfileModel> GetFollowing(int id)
        {
            List<ProfileModel> profiles = new List<ProfileModel>();
            ProfileModel profileM;

            var result = from p in db.Profiles
                         join f in db.Followers on p.ProfileId equals f.FollowingId
                         where f.FollowerId == id
                         select p;

            foreach (var p in result)
            {
                profileM = ConvertToModel(p);
                CheckIfFollowing(id, profileM);
                profiles.Add(profileM);
            }

            return profiles;
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
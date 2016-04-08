using ConnectR.Models;
using ConnectR.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConnectR.ServiceControllers
{
    public class FollowServiceController : ApiController
    {
        ProfileRepository repo = new ProfileRepository();

        // GET: api/FollowService/GetFollowers/userId
        [ActionName("GetFollowers")]
        public IEnumerable<ProfileModel> GetFollowers(int id)
        {
            return repo.GetFollowers(id);
        }

        // GET: api/FollowService/GetFollowing/userId
        [ActionName("GetFollowing")]
        public IEnumerable<ProfileModel> GetFollowing(int id)
        {
            return repo.GetFollowing(id);
        }

        // POST: api/FollowService/
        public void PostFollow(JObject followObject)
        {
            
            if((bool)followObject["follow"] == true)
            {
                repo.FollowProfile((int)followObject["followerId"], (int)followObject["followingId"]);
            }
            else
            {
                repo.UnfollowProfile((int)followObject["followerId"], (int)followObject["followingId"]);
            }
            
        }
    }
}

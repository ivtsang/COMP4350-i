using ConnectR.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ConnectR.Controllers
{
    public class FollowController : Controller
    {
        private string baseUri = WebConfigurationManager.AppSettings["ServiceUrl"] + "FollowService";

        public async Task<ActionResult> Followers(int id)
        {
            int profileId = await GetCurrentProfileId();
            ViewBag.ProfileId = profileId;
            string uri = baseUri + "/GetFollowers/" + id.ToString();

            IEnumerable<ProfileModel> profiles;

            using (HttpClient httpClient = new HttpClient())
            {

                profiles = JsonConvert.DeserializeObject<List<ProfileModel>>(
                    await httpClient.GetStringAsync(uri)
                );
            }

            return View(profiles);
        }

        public async Task<ActionResult> Following(int id)
        {
            int profileId = await GetCurrentProfileId();
            ViewBag.ProfileId = profileId;
            string uri = baseUri + "/GetFollowing/" + id.ToString();

            IEnumerable<ProfileModel> profiles;

            using (HttpClient httpClient = new HttpClient())
            {

                profiles = JsonConvert.DeserializeObject<List<ProfileModel>>(
                    await httpClient.GetStringAsync(uri)
                );
            }

            return View(profiles);
        }

        public async Task<ActionResult> Follow(int followingId, bool follow)
        {
            int followerId = await GetCurrentProfileId();
            JObject followObject = new JObject();
            string uri = baseUri + "/Follow";

            followObject.Add("followingId", followingId);
            followObject.Add("followerId", followerId);
            followObject.Add("follow", follow);

            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsJsonAsync<JObject>(uri, followObject);
                if (result.IsSuccessStatusCode)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    return View("Error");
                }
            }
        }

        public async Task<int> GetCurrentProfileId()
        {
            string userId = User.Identity.GetUserId();
            if (userId != null)
            {
                string uri = WebConfigurationManager.AppSettings["ServiceUrl"] + "ProfilesService/GetProfileByUserId/" + userId;
                ProfileModel profile;

                using (HttpClient httpClient = new HttpClient())
                {
                    profile = JsonConvert.DeserializeObject<ProfileModel>(
                        await httpClient.GetStringAsync(uri)
                    );
                }
                if (profile == null)
                    return 0;
                return profile.ProfileId;
            }

            return 0;
        }
    }
}
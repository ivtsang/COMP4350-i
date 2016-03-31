using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConnectR.Models;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Configuration;
using ConnectR.Repositories;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;

namespace ConnectR.Controllers
{
    public class ProfilesController : Controller
    {
        private Entities db = new Entities();
        private ProfileRepository repo = new ProfileRepository();
        private string baseUri = WebConfigurationManager.AppSettings["ServiceUrl"] + "ProfilesService";

        protected override void Initialize(RequestContext requestContext)
        {
            /*if (requestContext.RouteData.GetRequiredString("action").Equals("create", StringComparison.InvariantCultureIgnoreCase))
            {
                int? profileID = db.Profiles.Max(p => p.ProfileId);
                if (profileID.HasValue)
                    ViewBag.ProfileID = profileID + 1;
                else
                    ViewBag.ProfileID = 1;
            }*/
            base.Initialize(requestContext);
        }

        // GET: Profiles
        public async Task<ActionResult> Index()
        {
            //ViewBag.UserId = User.Identity.GetUserId();
            int profileId = await GetCurrentProfileId();
            ViewBag.ProfileId = profileId;

            List<ProfileModel> profiles;

            using (HttpClient httpClient = new HttpClient())
            {

                profiles = JsonConvert.DeserializeObject<List<ProfileModel>>(
                    await httpClient.GetStringAsync(baseUri)
                );
            }

            foreach(ProfileModel p in profiles)
            {
                repo.CheckIfFollowing(profileId, p);
            }

            return View(profiles);
        }

        // GET: Profiles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                id = await GetCurrentProfileId();
            }

            ProfileModel profile;
            string profileId = id.ToString();
            string uri = baseUri + "/GetProfile/" + profileId;

            using (HttpClient httpClient = new HttpClient())
            {
                profile = JsonConvert.DeserializeObject<ProfileModel>(
                    await httpClient.GetStringAsync(uri)
                );
            }
            if (profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = User.Identity.GetUserId();
            return View(profile);
        }

        // GET: Profiles/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            string userId = User.Identity.GetUserId();
            ProfileRepository repo = new ProfileRepository();
            Profile newProfile = new Profile
            {
                UserId = User.Identity.GetUserId()
            };
            newProfile = repo.SaveProfile(newProfile);
            //Profile profile = new Profile { UserId = User.Identity.GetUserId() };
            return View(newProfile);
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,FirstName,LastName,Age,Country,City,School,Degree")] Profile profile, HttpPostedFileBase upload)
        {
            File avatar;
            if (upload != null && upload.ContentLength > 0)
            {
                avatar = new File();
                avatar.FileName = System.IO.Path.GetFileName(upload.FileName);
                avatar.FileType = (short)FileType.Picture;
                avatar.ContentType = upload.ContentType;

                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    avatar.Content = reader.ReadBytes(upload.ContentLength);
                }
                profile.Files = new List<File> { avatar };
            }
            else
                avatar = null;
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsJsonAsync<Profile>(baseUri, profile);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }
        }

        // GET: Profiles/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProfileModel profile;
            string profileId = id.ToString();
            string uri = baseUri + "/GetProfile/" + profileId;

            using (HttpClient httpClient = new HttpClient())
            {
                profile = JsonConvert.DeserializeObject<ProfileModel>(
                    await httpClient.GetStringAsync(uri)
                );
            }
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,ProfileId,FirstName,LastName,Age,Country,City,School,Degree,About")] Profile profile, HttpPostedFileBase upload)
        {
            File avatar;
            if (upload != null && upload.ContentLength > 0)
            {
                avatar = new File();
                avatar.FileName = System.IO.Path.GetFileName(upload.FileName);
                avatar.FileType = (short)FileType.Picture;
                avatar.ContentType = upload.ContentType;

                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    avatar.Content = reader.ReadBytes(upload.ContentLength);
                }
                profile.Files = new List<File> { avatar };
            }
            else
                avatar = null;
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.PutAsJsonAsync<Profile>(baseUri, profile);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }
        }

        // GET: Profiles/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModel profile;
            string profileId = id.ToString();
            string uri = baseUri + "/GetProfile/" + profileId;

            using (HttpClient httpClient = new HttpClient())
            {
                profile = JsonConvert.DeserializeObject<ProfileModel>(
                    await httpClient.GetStringAsync(uri)
                );
            }
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string uri = baseUri + "/" + id;
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.DeleteAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    var manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var currentUser = manager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                    await manager.DeleteAsync(currentUser);
                    HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error");
                }
            }
        }

        public async Task<ActionResult> Follow(int followingId, bool follow)
        {
            int followerId = await GetCurrentProfileId();

            if (follow)
            {
                repo.FollowProfile(followerId, followingId);
            }
            else
            {
                repo.UnfollowProfile(followerId, followingId);
            }
            

            return RedirectToAction("Index");
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

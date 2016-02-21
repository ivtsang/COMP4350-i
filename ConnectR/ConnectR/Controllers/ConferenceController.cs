using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConnectR.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ConnectR.Controllers
{
    public class ConferenceController : Controller
    {
        private Entities db = new Entities();
        private string baseUri = "http://localhost:49950/api/ConferencesService";

        // GET: Conference
        public async Task<ActionResult> Index()
        {
            List<ConferenceModel> conferences;

            using (HttpClient httpClient = new HttpClient())
            {

                conferences = JsonConvert.DeserializeObject<List<ConferenceModel>>(
                    await httpClient.GetStringAsync(baseUri)
                );
            }

            return View(conferences);
        }

        // GET: Conference/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            int profileId = await GetCurrentProfileId();
            ViewBag.currentUser = profileId;
            string conferenceId = id.ToString();
            ConferenceModel conference;
            string uri = baseUri + "/" + conferenceId;

            using (HttpClient httpClient = new HttpClient())
            {
                conference = JsonConvert.DeserializeObject<ConferenceModel>(
                    await httpClient.GetStringAsync(uri)
                );
            }

            return View(conference);
        }

        // GET: Conference/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            ConferenceModel newConference = new ConferenceModel();
            newConference.ProfileId = await GetCurrentProfileId();
            newConference.Date = new DateTime(2016, 01, 01);
            return View(newConference);
        }

        // POST: Conference/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "Image")] ConferenceModel conference)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsJsonAsync<ConferenceModel>(baseUri, conference);
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }

        }

        // GET: Conference/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            string conferenceId = id.ToString();
            ConferenceModel conference;
            string uri = baseUri + "/" + conferenceId;

            using (HttpClient httpClient = new HttpClient())
            {
                conference = JsonConvert.DeserializeObject<ConferenceModel>(
                    await httpClient.GetStringAsync(uri)
                );
            }
            
            return View(conference);
        }

        // POST: Conference/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Exclude = "Image")] ConferenceModel conference)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.PutAsJsonAsync<ConferenceModel>(baseUri, conference);
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

        // GET: Conference/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
        }

        // POST: Conference/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string uri = baseUri + "/" + id;
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.DeleteAsync(uri);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<int> GetCurrentProfileId()
        {
            string userId = User.Identity.GetUserId();
            if(userId != null)
            {
                string uri = "http://localhost:49950/api/ProfilesService/GetProfileByUserId/" + userId;
                ProfileModel profile;

                using (HttpClient httpClient = new HttpClient())
                {
                    profile = JsonConvert.DeserializeObject<ProfileModel>(
                        await httpClient.GetStringAsync(uri)
                    );
                }
                return profile.ProfileId;
            }

            return 0;
        }

        public IEnumerable<Conference> SearchConference(Conference conference)
        {
            IEnumerable<Conference> result = from c in db.Conferences
                                where (conference.Title != null && c.Title == conference.Title)
                                where (conference.Location != null && c.Location == conference.Location)
                                where (conference.Date != null && c.Date == conference.Date)
                                select c;

            return result;
        }
    }
}

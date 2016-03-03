using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConnectR.Models;
using ConnectR.Repositories;

namespace ConnectR.ServiceControllers
{
    public class ProfilesServiceController : ApiController
    {
        private Entities db = new Entities();
        ProfileRepository repo = new ProfileRepository();

        // GET: api/ProfilesService
        public IEnumerable<ProfileModel> GetProfiles()
        {
            return null;
        }

        // GET: api/ProfilesService/5
        public ProfileModel GetProfile(int id)
        {
            return null;
        }

        // GET: api/ProfilesService/GetProfileByUserId/userId
        [ActionName("GetProfileByUserId")]
        public ProfileModel GetProfileByUserId(string id)
        {
            return repo.GetProfileByUserId(id);
        }

        // PUT: api/ProfilesService/5
        public void PutProfile(ProfileModel Profile)
        {
        }

        // POST: api/ProfilesService
        public void PostProfile(ProfileModel Profile)
        {
        }

        // DELETE: api/ProfilesService/5
        public void DeleteProfile(int id)
        {
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

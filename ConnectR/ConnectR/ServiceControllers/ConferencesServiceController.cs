using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ConnectR.Models;
using ConnectR.Repositories;

namespace ConnectR.ServiceControllers
{
    public class ConferencesServiceController : ApiController
    {
        private Entities db = new Entities();
        ConferenceRepository repo = new ConferenceRepository();

        // GET: api/ConferencesService
        public IEnumerable<ConferenceModel> GetConferences()
        {
            return repo.GetConferences();
        }

        // GET: api/ConferencesService/5
        public ConferenceModel GetConference(int id)
        {
            return repo.GetConferenceById(id);
        }

        // PUT: api/ConferencesService/5
        public void PutConference(ConferenceModel conference)
        {
            repo.UpdateConference(conference);
        }

        // POST: api/ConferencesService
        public void PostConference(ConferenceModel conference)
        {
            repo.SaveConference(conference);
        }

        // DELETE: api/ConferencesService/5
        public void DeleteConference(int id)
        {
            repo.DeleteConference(id);
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
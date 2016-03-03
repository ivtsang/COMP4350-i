using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConnectR.Models;
using System.Data.Entity;

namespace ConnectR.Repositories
{
    public class ConferenceRepository
    {
        private Entities db = new Entities();

        public IEnumerable<ConferenceModel> GetConferences()
        {
            List<ConferenceModel> conferences = new List<ConferenceModel>();
            foreach (var c in db.Conferences)
            {
                conferences.Add(ConvertToModel(c));
            }
            return conferences.OrderBy(c => c.Date);
        }

        public ConferenceModel GetConferenceById(int id)
        {
            Conference c = db.Conferences.Find(id);
            return ConvertToModel(c);
        }

        public void SaveConference(ConferenceModel conference)
        {
            db.Conferences.Add(ConvertToConference(conference));
            db.SaveChanges();
        }

        public void DeleteConference(int id)
        {
            Conference conference = db.Conferences.Find(id);
            db.Conferences.Remove(conference);
            db.SaveChangesAsync();
        }

        public void UpdateConference(ConferenceModel conference)
        {
            Conference c = ConvertToConference(conference);

            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }


        public IEnumerable<Conference> SearchConference(ConferenceModel conference)
        {
            IEnumerable<Conference> result = from c in db.Conferences
                                             where (conference.Title != null && c.Title == conference.Title)
                                             where (conference.Location != null && c.Location == conference.Location)
                                             where (conference.Date != null && c.Date == conference.Date)
                                             select c;

            return result;
        }

        public Conference ConvertToConference(ConferenceModel conference)
        {
            Conference c = new Conference
            {
                ConferenceId = conference.ConferenceId,
                ProfileId = conference.ProfileId,
                Title = conference.Title,
                Content = conference.Content,
                Location = conference.Location,
                Date = conference.Date,
            };

            return c;
        }

        public ConferenceModel ConvertToModel(Conference c)
        {
            ConferenceModel conference = new ConferenceModel
            {
                ConferenceId = c.ConferenceId,
                ProfileId = c.ProfileId,
                FirstName = c.Profile.FirstName,
                LastName = c.Profile.LastName,
                Title = c.Title,
                Content = c.Content,
                Location = c.Location,
                Date = c.Date.Value
            };

            return conference;
        }
    }
}
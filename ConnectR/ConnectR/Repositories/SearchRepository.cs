using ConnectR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ConnectR.Repositories
{
    public class SearchRepository
    {
        private Entities db = new Entities();
        List<Profile> profiles = null;
        List<Conference> conferences = null;

        public SearchViewModel GetSearchViewModel(string search, string category)
        {
            if (category == "profile")
            {
                profiles = db.Profiles.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search)).ToList();
            }
            else if (category == "conference")
            {
                conferences = db.Conferences.Where(p => p.Title.Contains(search)).ToList();
            }
            else
            {
                profiles = db.Profiles.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search)).ToList();
                conferences = db.Conferences.Where(p => p.Title.Contains(search)).ToList();
            }
            
            

            SearchViewModel searchModel = new SearchViewModel();
            searchModel.keyword = search;
            searchModel.profiles = profiles;
            searchModel.conferences = conferences;

            return searchModel;


        }
    }
}
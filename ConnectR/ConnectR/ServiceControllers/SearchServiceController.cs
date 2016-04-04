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
    public class SearchServiceController : ApiController
    {
        SearchRepository repo = new SearchRepository();
        public SearchViewModel GetSearchResult(string keyword, string category)
        {
            return repo.GetSearchViewModel(keyword, category);
        }
    }
}

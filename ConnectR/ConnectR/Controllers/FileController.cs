using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ConnectR.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace ConnectR.Controllers
{
    public class FileController : Controller
    {
        private string baseUri = WebConfigurationManager.AppSettings["ServiceUrl"] + "FilesService";
        // GET: File
        public async Task<ActionResult> Index(int id)
        {
            FileModel file;
            string imgId = id.ToString();
            string uri = baseUri + "/"+ imgId;

            using (HttpClient httpClient = new HttpClient())
            {
                file = JsonConvert.DeserializeObject<FileModel>(
                    await httpClient.GetStringAsync(uri)
                );
            }
            return File(file.Content, file.ContentType);
        }
    }
}
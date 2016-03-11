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
    public class FilesServiceController : ApiController
    {
        private Entities db = new Entities();
        FileRepository repo = new FileRepository();

        // GET: api/FilesService
        public IEnumerable<FileModel> GetFiles()
        {
            //return repo.GetFiles();
            return null;
        }

        // GET: api/FilesService/5
        public FileModel GetFiles(int id)
        {
            return repo.GetFileById(id);
        }

        // PUT: api/FilesService/5
        public void PutFiles(FileModel conference)
        {
            //repo.UpdateFiles(conference);
        }

        // POST: api/FilesService
        public void PostFiles(FileModel conference)
        {
            //repo.SaveFiles(conference);
        }

        // DELETE: api/FilesService/5
        public void DeleteFiles(int id)
        {
            //repo.DeleteFiles(id);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ConnectR.Models;
using System.Threading.Tasks;

namespace ConnectR.Controllers
{
    public class FileController : Controller
    {
        private Entities db = new Entities();
        // GET: File
        public async Task<ActionResult> Index(int id)
        {
            var file = await db.Files.FindAsync(id);
            return File(file.Content, file.ContentType);
        }
    }
}
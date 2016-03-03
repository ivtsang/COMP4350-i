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

namespace ConnectR.Controllers
{
    public class ConversationController : Controller
    {
        private Entities db = new Entities();

        // GET: Conversation
        public async Task<ActionResult> Index()
        {
            return View(await db.Conversations.ToListAsync());
        }

        // GET: Conversation/Create
        public ActionResult Create()
        {           
            return View();
        }

        // POST: Conversation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ConversationId")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                db.Conversations.Add(conversation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(conversation);
        }

        // GET: Conference/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversation conv = db.Conversations.Find(id);
            if (conv == null)
            {
                return HttpNotFound();
            }
            return View("Details", conv);
        }

        // GET: Conversation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversation conversation = await db.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return HttpNotFound();
            }
            return View(conversation);
        }

        // POST: Conversation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Conversation conversation = await db.Conversations.FindAsync(id);
            db.Conversations.Remove(conversation);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

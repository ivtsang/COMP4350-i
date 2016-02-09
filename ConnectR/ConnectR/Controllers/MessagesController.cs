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
using Microsoft.AspNet.Identity;

namespace ConnectR.Controllers
{
    public class MessagesController : Controller
    {
        private Entities db = new Entities();

        // GET: Messages
        public async Task<ActionResult> Index()
        {
            var messages = db.Messages.Include(m => m.Conversation).Include(m => m.Profile);
            return View(await messages.ToListAsync());
        }

        public async Task<ActionResult> MessageUsers()
        {
            var messages = db.Profiles;
            return View(await messages.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        [Authorize]
        public ActionResult Create(Conversation convo, int receiverId)
        {
            string userId = User.Identity.GetUserId();
            var query = from p in db.Profiles
                        where p.UserId == userId
                        select p;
            Profile profile = query.ToList().First();

            Message msg = new Message();
            if(convo == null || convo.ConversationId <= 0)
            {
                convo = db.Conversations.Add(new Conversation());
                db.SaveChanges();
            }
            if(receiverId != 0)
            {
                Participant participant = new Participant { ConversationId = convo.ConversationId, ProfileId = receiverId };
                db.Participants.Add(participant);
            }
            Participant sender = new Participant { ConversationId = convo.ConversationId, ProfileId = profile.ProfileId };
            db.Participants.Add(sender);
            db.SaveChanges();
            msg.ProfileId = profile.ProfileId;
            msg.ConversationId = convo.ConversationId;
            return View(msg);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MessageId,ConversationId,ProfileId,Date,Text")] Message message)
        {
            message.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ConversationId = new SelectList(db.Conversations, "ConversationId", "ConversationId", message.ConversationId);
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "UserId", message.ProfileId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConversationId = new SelectList(db.Conversations, "ConversationId", "ConversationId", message.ConversationId);
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "UserId", message.ProfileId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MessageId,ConversationId,ProfileId,Date,Text")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ConversationId = new SelectList(db.Conversations, "ConversationId", "ConversationId", message.ConversationId);
            ViewBag.ProfileId = new SelectList(db.Profiles, "ProfileId", "UserId", message.ProfileId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Message message = await db.Messages.FindAsync(id);
            db.Messages.Remove(message);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConnectR.Models;
using Microsoft.AspNet.Identity;

namespace ConnectR.Controllers
{
    public class ConversationController : Controller
    {
        private Entities db = new Entities();

        public PartialViewResult CreateNewMessage()
        {
            return PartialView("CreateNewMessage");
        }

        // GET: Conversation
        public ActionResult Index()
        {
            return View(db.Conversations.ToList());
        }

        // GET: Conversation/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View("../Message/Create");
        }

        // POST: Conversation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "msg_id,conv_id,user_id,msg_date,msg_content")] Message message)
        {
            Conversation conv = new Conversation();
            if (ModelState.IsValid)
            {
                db.Conversations.Add(conv);
                db.SaveChanges();

            }

            message.conv_id = conv.conv_id;
            message.user_id = User.Identity.GetUserId();
            message.msg_date = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", message.user_id);
            ViewBag.conv_id = new SelectList(db.Conversations, "conv_id", "conv_id", message.conv_id);
            return View(message);
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

        // GET: Conversation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversation conversation = db.Conversations.Find(id);
            if (conversation == null)
            {
                return HttpNotFound();
            }
            return View(conversation);
        }

        // POST: Conversation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "conv_id")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conversation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conversation);
        }

        // GET: Conversation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conversation conversation = db.Conversations.Find(id);
            if (conversation == null)
            {
                return HttpNotFound();
            }
            return View(conversation);
        }

        // POST: Conversation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Conversation conversation = db.Conversations.Find(id);
            db.Conversations.Remove(conversation);
            db.SaveChanges();
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

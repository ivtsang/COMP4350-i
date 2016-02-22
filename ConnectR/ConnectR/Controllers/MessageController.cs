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
    public class MessageController : Controller
    {
        private Entities db = new Entities();

        // GET: Message
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.AspNetUser).Include(m => m.Conversation);
            return View(messages.ToList());
        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.conv_id = new SelectList(db.Conversations, "conv_id", "conv_id");
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            Message message = new Message();

            var yeah = form.Get("conv_id");

            message.conv_id = int.Parse(form.Get("conv_id"));
            message.msg_content = form.Get("msg_content");

            message.user_id = User.Identity.GetUserId();
            message.msg_date = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Details", "Conversation", new { id = message.conv_id });
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", message.user_id);
            ViewBag.conv_id = new SelectList(db.Conversations, "conv_id", "conv_id", message.conv_id);
            return View();
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", message.user_id);
            ViewBag.conv_id = new SelectList(db.Conversations, "conv_id", "conv_id", message.conv_id);
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "msg_id,conv_id,user_id,msg_date,msg_content")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", message.user_id);
            ViewBag.conv_id = new SelectList(db.Conversations, "conv_id", "conv_id", message.conv_id);
            return View(message);
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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

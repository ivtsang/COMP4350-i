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
    public class MessageController : Controller
    {
        private Entities db = new Entities();

        // POST: Messages/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection form)
        {
            Message message = new Message();

            message.ConversationId = int.Parse(form.Get("ConversationId"));
            message.Text = form.Get("Text");

            message.SenderName = User.Identity.Name;
            message.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Conversation", new { id = message.ConversationId });
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

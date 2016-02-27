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
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace ConnectR.Controllers
{
    public class ProfilesController : Controller
    {
        private Entities db = new Entities();

        protected override void Initialize(RequestContext requestContext)
        {
            /*if (requestContext.RouteData.GetRequiredString("action").Equals("create", StringComparison.InvariantCultureIgnoreCase))
            {
                int? profileID = db.Profiles.Max(p => p.ProfileId);
                if (profileID.HasValue)
                    ViewBag.ProfileID = profileID + 1;
                else
                    ViewBag.ProfileID = 1;
            }*/
            base.Initialize(requestContext);
        }

        // GET: Profiles
        public async Task<ActionResult> Index()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View(await db.Profiles.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = await db.Profiles.Include("Files").SingleOrDefaultAsync(p => p.ProfileId == id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = User.Identity.GetUserId();
            return View(profile);
        }

        // GET: Profiles/Create
        [Authorize]
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            if (db.Profiles.Any(p => p.UserId == userId))
                return RedirectToAction("Index");
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,FirstName,LastName,Age,Country,City,School,Degree")] Profile profile, HttpPostedFileBase upload)
        {
            try {

                if (ModelState.IsValid)
                {
                    File avatar;
                    if (upload != null && upload.ContentLength > 0)
                    {
                        avatar = new File();
                        avatar.FileName = System.IO.Path.GetFileName(upload.FileName);
                        avatar.FileType = (short)FileType.Picture;
                        avatar.ContentType = upload.ContentType;

                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        profile.Files = new List<File> { avatar };
                    }
                    else
                        avatar = null;
                    profile.UserId = User.Identity.GetUserId();
                    db.Profiles.Add(profile);
                    await db.SaveChangesAsync();
                    if (avatar != null)
                    {
                        profile.UserImage = avatar.Id;
                        await db.SaveChangesAsync();
                    }
                    return RedirectToAction("Index");
                }

                return View(profile);
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }

                return View(profile);
            }
        }

        // GET: Profiles/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Profile profile = await db.Profiles.FindAsync(id);
            Profile profile = await db.Profiles.Include("Files").SingleOrDefaultAsync(p => p.ProfileId == id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "FirstName,LastName,Age,Country,City,School,Degree")] Profile profile, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    if (profile.UserImage > 0)
                    {
                        db.Files.Remove(profile.Files.First(f => f.Id == profile.UserImage));
                    }
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = (short)FileType.Picture,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    profile.Files.Add(avatar);
                    await db.SaveChangesAsync();
                    profile.UserImage = avatar.Id;
                    await db.SaveChangesAsync();
                }

                db.Entry(profile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = await db.Profiles.FindAsync(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Profile profile = await db.Profiles.FindAsync(id);
            db.Profiles.Remove(profile);
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

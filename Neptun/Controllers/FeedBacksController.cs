using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Neptun.Models;
using Neptun.Persistence;

namespace Neptun.Controllers
{
    public class FeedBacksController : Controller
    {
        private DataContext db = new DataContext();

        // GET: FeedBacks
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Id,LastName,FirstName,PhoneNuber,Email,Message")] FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                db.FeedBacks.Add(feedBack);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<ActionResult> AdminInfo()
        {
            return View(await db.FeedBacks.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedBack feedBack = await db.FeedBacks.FindAsync(id);
            if (feedBack == null)
            {
                return HttpNotFound();
            }
            return View(feedBack);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FeedBack feedBack = await db.FeedBacks.FindAsync(id);
            db.FeedBacks.Remove(feedBack);
            await db.SaveChangesAsync();
            return RedirectToAction("AdminInfo");
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

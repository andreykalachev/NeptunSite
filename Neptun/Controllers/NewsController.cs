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
using Neptun.Models.ViewModels;
using Neptun.Servises;

namespace Neptun.Controllers
{
    public class NewsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: News
        public async Task<ActionResult> Index(int? page)
        {
            const int itemsPerPage = 12;
            var currentPageIndex = page ?? 0;
            var pageCount = (int)Math.Ceiling((double)db.News.Count() / itemsPerPage);

            if (currentPageIndex < 0 || currentPageIndex >= pageCount)
            {
                return HttpNotFound();
            }

            var view = new NewsIndexViewModel(pageCount, currentPageIndex);
            var news = db.News.OrderByDescending(x => x.Date);
            view.News = await news.Skip(currentPageIndex * itemsPerPage).Take(itemsPerPage).ToListAsync();

            return View(view);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminInfo()
        {
            var news = await db.News.ToListAsync();
            return View(news.OrderByDescending(x => x.Date));
        }

        // GET: News/Details/5

        // GET: News/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Date,Description,Photo,HttpPostedFilePhoto")] NewsCreateEditViewModel newsViewModel)
        {
            if (ModelState.IsValid)
            {
                News news = newsViewModel;
                news.Photo = FilesOperations.SaveImg(newsViewModel.HttpPostedFilePhoto);
                db.News.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction("AdminInfo");
            }

            return View(newsViewModel);
        }

        // GET: News/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View((NewsCreateEditViewModel)news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Date,Description,Photo,HttpPostedFilePhoto")] NewsCreateEditViewModel newsViewModel)
        {

            if (ModelState.IsValid)
            {
                News news = newsViewModel;

                if (newsViewModel.HttpPostedFilePhoto != null && newsViewModel.HttpPostedFilePhoto.ContentLength > 0)
                {
                    FilesOperations.DeleteFile(news.Photo);
                    news.Photo = FilesOperations.SaveImg(newsViewModel.HttpPostedFilePhoto);
                }

                db.Entry(news).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AdminInfo");
            }

            return View(newsViewModel);
        }

        // GET: News/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            FilesOperations.DeleteFile(news.Photo);
            db.News.Remove(news);
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

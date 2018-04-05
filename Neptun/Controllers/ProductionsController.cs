using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Neptun.Models;
using Neptun.Models.Enum;
using Neptun.Models.ViewModels;
using Neptun.Persistence;
using Neptun.Servises;

namespace Neptun.Controllers
{
    public class ProductionsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Productions
        public async Task<ActionResult> Index(int? page, ProductType? productType)
        {
            const int itemsPerPage = 12;
            var currentPageIndex = page ?? 0;
            var pageCount = productType == null ? db.Productions.Count() : db.Productions.Count(x => x.ProductType == productType);
            pageCount = (int) Math.Ceiling((double) pageCount / itemsPerPage);

            if (pageCount == 0) return Redirect("/Productions/EmptyProductionPage");

            if (currentPageIndex < 0 || currentPageIndex >= pageCount)
            {
                return HttpNotFound();
            }

            var view = new ProductIndexViewModel(pageCount, currentPageIndex, productType);

            var query = db.Productions.OrderBy(x => x.Id).Select(x => new ProductionListViewModel
            {
                Id = x.Id,
                Title = x.Title,
                FullDescriptionPdf = x.FullDescriptionPdf,
                Photo = x.Photo,
                ButtonDescriptionName = x.ButtonDescriptionName,
                ProductType = x.ProductType
            });

            if (productType != null)
            {
                query = query.Where(x => x.ProductType == productType);
            }

            view.Products = await query.Skip(currentPageIndex * itemsPerPage).Take(itemsPerPage).ToListAsync();

            return View(view);
        }

        public ActionResult EmptyProductionPage()
        {
            return View();
        }

        public ActionResult Cabinets()
        {
            return View();
        }

        public ActionResult TreatmentFacilities()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminInfo()
        {
            return View(await db.Productions.Select(x => new ProductionListViewModel
            {
                Id = x.Id,
                Title = x.Title,
                FullDescriptionPdf = x.FullDescriptionPdf,
                Photo = x.Photo,
                ButtonDescriptionName = x.ButtonDescriptionName,
                ProductType = x.ProductType
            }).ToListAsync());
        }

        public async Task<ActionResult> Info(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var production = await db.Productions.FindAsync(id);
            if (production == null)
            {
                return HttpNotFound();
            }

            return View(production);

        }

        // GET: Productions/Details/5
        public ActionResult DisplayPdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Production production = db.Productions.Find(id);
            if (string.IsNullOrEmpty(production?.ButtonDescriptionName) || production.FullDescriptionPdf.Length == 0)
            {
                return HttpNotFound();
            }
            var pdfPath = System.Web.HttpContext.Current.Server.MapPath(production.FullDescriptionPdf);

            if (System.IO.File.Exists(pdfPath)) HttpNotFound();

            var fileContents = System.IO.File.ReadAllBytes(pdfPath);

            return new FileContentResult(fileContents, "application/pdf");
        }

        public ActionResult DisplayPdfFromFile(string path)
        {
            var pdfPath = System.Web.HttpContext.Current.Server.MapPath(path);

            if (System.IO.File.Exists(pdfPath)) HttpNotFound();

            var fileContents = System.IO.File.ReadAllBytes(pdfPath);

            return new FileContentResult(fileContents, "application/pdf");
        }

        // GET: Productions/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Id,Title,Description,FullDescriptionPdf,ButtonDescriptionName,ProductType,Photo, HttpPostedFilePhoto, HttpPostedFilePdf," +
                            "PageTitle, PageDescription, PageKeywords")]
            ProductionCreateEditViewModel productionViewModel)
        {
            if (ModelState.IsValid)
            {
                Production production = productionViewModel;
                production.Photo = FilesOperations.SaveImg(productionViewModel.HttpPostedFilePhoto);
                if (!string.IsNullOrEmpty(production.ButtonDescriptionName))
                    production.FullDescriptionPdf = FilesOperations.SavePdf(productionViewModel.HttpPostedFilePdf);

                db.Productions.Add(production);
                await db.SaveChangesAsync();
                return RedirectToAction("AdminInfo");
            }

            return View(productionViewModel);
        }

        // GET: Productions/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Production production = await db.Productions.FindAsync(id);
            if (production == null)
            {
                return HttpNotFound();
            }

            return View((ProductionCreateEditViewModel)production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,Title,Description,FullDescriptionPdf,ButtonDescriptionName,ProductType,Photo, HttpPostedFilePhoto, HttpPostedFilePdf," +
                            "PageTitle, PageDescription, PageKeywords")]
            ProductionCreateEditViewModel productionViewModel)
        {
            if (ModelState.IsValid)
            {
                Production production = productionViewModel;

                if (productionViewModel.HttpPostedFilePhoto != null && productionViewModel.HttpPostedFilePhoto.ContentLength > 0)
                {
                    FilesOperations.DeleteFile(production.Photo);
                    production.Photo = FilesOperations.SaveImg(productionViewModel.HttpPostedFilePhoto);
                }


                if (!string.IsNullOrEmpty(productionViewModel.ButtonDescriptionName))
                {
                    if (productionViewModel.HttpPostedFilePdf != null && productionViewModel.HttpPostedFilePdf.ContentLength > 0)
                    {
                        FilesOperations.DeleteFile(production.FullDescriptionPdf);
                        production.FullDescriptionPdf = FilesOperations.SavePdf(productionViewModel.HttpPostedFilePdf);
                    }
                }
                else FilesOperations.DeleteFile(production.FullDescriptionPdf);

                db.Entry(production).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AdminInfo");
            }

            return View(productionViewModel);
        }

        // GET: Productions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var production = await db.Productions.FindAsync(id);
            if (production == null)
            {
                return HttpNotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var production = await db.Productions.FindAsync(id);
            FilesOperations.DeleteFile(production.Photo);
            FilesOperations.DeleteFile(production.FullDescriptionPdf);
            db.Productions.Remove(production);
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
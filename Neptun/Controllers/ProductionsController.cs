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
using Neptun.Models.DTO;
using Neptun.Models.Enum;
using Neptun.Models.ViewModels.Product;
using Neptun.Persistence;

namespace Neptun.Controllers
{
    public class ProductionsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Productions
        public async Task<ActionResult> Index(int? page, ProductType? productType)
        {
            const int itemsPerPage = 9; 
            var currentPageIndex = page ?? 0;
            var pageCount = (int)Math.Ceiling((double)db.Productions.Count() / itemsPerPage);
            if (currentPageIndex < 0 || currentPageIndex >= pageCount)
            {
                return HttpNotFound();
            }

            var view = new ProductIndexViewModel
            {
                CurrentPage = currentPageIndex,
                PageCount = pageCount,
                ProductType = productType
            };

            IQueryable<ProductIndexDto> query;

            if (productType != null)
            {
                query = db.Productions.Where(x => x.ProductType == productType).Select(x =>
                    new ProductIndexDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ProductType = x.ProductType,
                        Photo = x.Photo
                    });
            }
            else
            {
                query = db.Productions.Select(x =>
                    new ProductIndexDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ProductType = x.ProductType,
                        Photo = x.Photo
                    });

            }

            view.Products = await query.OrderBy(x => x.Id).Skip(currentPageIndex * itemsPerPage).Take(itemsPerPage).ToListAsync();

            return View(view);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminInfo()
        {
            return View(await db.Productions.Select(x => new ProductAdminViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ProductType = x.ProductType,
                Photo = x.Photo,
                ButtonDescriptionName = x.ButtonDescriptionName
            }).ToListAsync());
        }

        public async Task<ActionResult> Info(int? id)
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

            var productionView = new ProductInfoViewModel
            {
                Id = production.Id,
                Title = production.Title,
                Description = production.Description,
                Photo = production.Photo,
                ButtonDescriptionName = production.ButtonDescriptionName
            };
            return View(productionView);

        }

        // GET: Productions/Details/5
        public ActionResult DisplayPdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Production production = db.Productions.Find(id);
            if (production == null || string.IsNullOrEmpty(production.ButtonDescriptionName) || production.FullDescriptionPdf.Length == 0)
            {
                return HttpNotFound();
            }

            var byteArray = production.FullDescriptionPdf;
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
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
            [Bind(Include = "Id,Title,Description,FullDescriptionPdf,ButtonDescriptionName,ProductType,Photo")]
            Production production, HttpPostedFileBase uploadPhoto, HttpPostedFileBase uploadPdf)
        {
            if (ModelState.IsValid)
            {
                if (uploadPhoto != null && uploadPhoto.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(uploadPhoto.InputStream))
                    {
                        production.Photo = reader.ReadBytes(uploadPhoto.ContentLength);
                    }
                }

                if (!string.IsNullOrEmpty(production.ButtonDescriptionName))
                {
                    if (uploadPdf != null && uploadPdf.ContentLength > 0)
                    {
                        using (var reader = new BinaryReader(uploadPdf.InputStream))
                        {
                            production.FullDescriptionPdf = reader.ReadBytes(uploadPdf.ContentLength);
                        }
                    }
                    else production.ButtonDescriptionName = string.Empty;
                }
                else production.FullDescriptionPdf = new byte[0];

                db.Productions.Add(production);
                await db.SaveChangesAsync();
                return RedirectToAction("AdminInfo");
            }

            return View(production);
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

            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,Title,Description,FullDescriptionPdf,ButtonDescriptionName,ProductType,Photo")]
            Production production, HttpPostedFileBase uploadPhoto, HttpPostedFileBase uploadPdf)
        {
            if (ModelState.IsValid)
            {
                if (uploadPhoto != null && uploadPhoto.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(uploadPhoto.InputStream))
                    {
                        production.Photo = reader.ReadBytes(uploadPhoto.ContentLength);
                    }
                }
                if (!string.IsNullOrEmpty(production.ButtonDescriptionName))
                {
                    if (uploadPdf != null && uploadPdf.ContentLength > 0)
                    {
                        using (var reader = new BinaryReader(uploadPdf.InputStream))
                        {
                            production.FullDescriptionPdf = reader.ReadBytes(uploadPdf.ContentLength);
                        }
                    }
                    else if(production.FullDescriptionPdf?.Length == 0) production.ButtonDescriptionName = string.Empty;
                }
                else production.FullDescriptionPdf = new byte[0];

                db.Entry(production).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AdminInfo");
            }

            return View(production);
        }

        // GET: Productions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var production = await db.Productions.Where(x => x.Id == id).Select(x => new ProductDeleteViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ProductType = x.ProductType,
            }).FirstAsync();
            if (production == null)
            {
                return HttpNotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Production production = await db.Productions.FindAsync(id);
            db.Productions.Remove(production);
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
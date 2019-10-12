using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Neptun.Models;
using Neptun.Models.Email;
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
        public async Task<ActionResult> Index([Bind(Include = "Id,LastName,FirstName,PhoneNuber,Email,Message")] FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                feedBack.Date = DateTime.Today;
                db.FeedBacks.Add(feedBack);
                await db.SaveChangesAsync();
                await AutoEmailNotification(feedBack);

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminInfo()
        {
            var feedBacks = await db.FeedBacks.ToListAsync();
            feedBacks.Reverse();
            return View(feedBacks);
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FeedBack feedBack = await db.FeedBacks.FindAsync(id);
            db.FeedBacks.Remove(feedBack);
            await db.SaveChangesAsync();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        private async Task AutoEmailNotification(FeedBack fb)
        {
            var smtp = new System.Net.Mail.SmtpClient("robots.1gb.ua", 25);

            var m = new MailMessage()
            {
                From = new MailAddress("tdneptun@neptun-odessa.com", "\"nepun-odessa.com\""),
                To = { new MailAddress("skalachev@soyuz-corp.com") },
                Subject = fb.Date?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                Body = $"<h4>Вам написал {fb.FirstName} {fb.LastName}  {fb.Date?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)}</h4>" +
                       $"<div><pre>Почта:\t{fb.Email}</pre></div>" +
                       $"<div><pre>Телефон:\t{fb.PhoneNuber}</pre></div>" +
                       $"<p><pre>Сообщение:\t{fb.Message}</pre></p>",
                IsBodyHtml = true
            };
            await smtp.SendMailAsync(m);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SendEmail([Bind(Include = "Email, Password, DestinationEmail, Header, Subject, Message")]EmailRespond respond)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(respond.Email, respond.Password)
            };

            var m = new MailMessage()
            {
                From = new MailAddress(respond.Email, respond.Header),
                To = { new MailAddress(respond.DestinationEmail) },
                Subject = respond.Subject,
                Body = respond.Message
            };
            await smtp.SendMailAsync(m);
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
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

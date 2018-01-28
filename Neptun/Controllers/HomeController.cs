using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Neptun.Models;
using Neptun.Persistence;

namespace Neptun.Controllers
{
    public class HomeController : Controller
    {

        private DataContext db = new DataContext();

        public async Task<ActionResult> Index()
        {
            var company = await db.Companies.FirstOrDefaultAsync();
            return View(company);
        }
    }
}
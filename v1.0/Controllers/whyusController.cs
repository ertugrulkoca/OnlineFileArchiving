using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using v1._0.Models;
namespace v1._0.Controllers
{
    public class whyusController : Controller
    {
        FileArchivingEntities db = new FileArchivingEntities();
        // GET: whyus
        public ActionResult whyus()
        {
            TempData["Ad2"] = TempData["Ad"];
            ViewBag.deger2 = TempData["Ad2"];
            var myQ = db.whyus.Where(h => h.id > 0).Select(h => h);
            return View(myQ);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using v1._0.Models;
namespace v1._0.Controllers
{
    public class priceController : Controller
    {
        FileArchivingEntities db = new FileArchivingEntities();
        // GET: price
        public ActionResult price()
        {
            var myQ = db.pricing250.Where(h => h.id > 0).Select(h => h);
            return View(myQ);
        }        
        public PartialViewResult price2()
        {
            var myQ2 = db.pricing1tb.Where(h => h.id > 0).Select(h => h);
            return PartialView(myQ2);
        }        
        public PartialViewResult price3()
        {
            var myQ3 = db.pricing4tb.Where(h => h.id > 0).Select(h => h);
            return PartialView(myQ3);
        }
    }
}
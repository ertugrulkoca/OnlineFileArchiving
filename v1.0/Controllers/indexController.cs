using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using v1._0.Models;

namespace v1._0.Controllers
{
    public class indexController : Controller
    {
        FileArchivingEntities db = new FileArchivingEntities();
        // GET: index
        public ActionResult homepage()
        {
            return View();
        }
        public ActionResult download()
        {
            return View();
        }
        public ActionResult kk()
        {
            return View();
        }
        public ActionResult contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult contact(contact c)
        {
            db.contact.Add(c);
            db.SaveChanges();
            return View();
        }
    }
}
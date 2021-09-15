using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using v1._0.Models;
namespace v1._0.Controllers
{
    public class adminController : Controller
    {
        FileArchivingEntities db = new FileArchivingEntities();
        // GET: admin
        [Authorize(Roles = "a")]
        public ActionResult admin()
        {
            var myQ = db.contact.Where(h => h.id > 0).Select(h => h);
            return View(myQ);
        }
        public ActionResult deletemessage(int id)
        {
            var du = db.contact.Find(id);
            db.contact.Remove(du);
            db.SaveChanges();
            return RedirectToAction("admin", "admin");
        }

        public ActionResult adminloginout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("signin", "sign");
        }




        [Authorize(Roles = "a")]
        public ActionResult user()
        {
            var myQ = db.users.Where(h => h.id > 0).Select(h => h);
            return View(myQ);
        }
        [Authorize(Roles = "a")]
        [HttpPost]
        public ActionResult user(FormCollection form)
        {
            adnnin model = new adnnin();
            model.mail = form["mail"].Trim();
            model.password = form["password"].Trim();
            db.adnnin.Add(model);
            db.SaveChanges();
            return RedirectToAction("user", "admin");
        }
        public ActionResult deleteuser(int id)
        {
            users userinDb = new users();
            var du = db.users.Find(id);
            db.users.Remove(du);
            db.SaveChanges();
            return RedirectToAction("user", "admin");
        }



        [Authorize(Roles = "a")]
        public PartialViewResult files()
        {
            var myQ = db.userfiles.Where(h => h.id > 0).Select(h => h);
            return PartialView(myQ);
        }
        public ActionResult deletefiles(int id)
        {
            users userinDb = new users();
            var df = db.userfiles.Find(id);
            db.userfiles.Remove(df);
            db.SaveChanges();
            return RedirectToAction("user", "admin");
        }



        [Authorize(Roles = "a")]
        public ActionResult whyus()
        {
            var myQ = db.whyus.Where(h => h.id > 0).Select(h => h);
            return View(myQ);
        }


        [Authorize(Roles = "a")]
        public ActionResult updatewhyus(int id)
        {
            var pr = db.whyus.Find(id);
            return View(pr);
        }
        [Authorize(Roles = "a")]
        [HttpPost]
        public ActionResult updatewhyus(whyus why)
        {
            int id = why.id;
            var updatedwhyus = db.whyus.Find(id);
            updatedwhyus.baslik = why.baslik;
            updatedwhyus.aciklama = why.aciklama;
            updatedwhyus.images = why.images;
            db.SaveChanges();
            return RedirectToAction("whyus", "admin");
        }


        [Authorize(Roles = "a")]
        public ActionResult price()
        {
            var myQ = db.pricing250.Where(h => h.id > 0).Select(h => h);
            return View(myQ);
        }
        [Authorize(Roles = "a")]
        public PartialViewResult price2()
        {
            var myQ = db.pricing1tb.Where(h => h.id > 0).Select(h => h);
            return PartialView(myQ);
        }
        [Authorize(Roles = "a")]
        public PartialViewResult price3()
        {
            var myQ = db.pricing4tb.Where(h => h.id > 0).Select(h => h);
            return PartialView(myQ);
        }

        [Authorize(Roles = "a")]
        public ActionResult updateprice250(int id)
        {
            var pr = db.pricing250.Find(id);
            return View(pr);
        }
        [Authorize(Roles = "a")]
        [HttpPost]
        public ActionResult updateprice250(pricing250 price)
        {
            int id = price.id;

            var updatedprice = db.pricing250.Find(id);
            updatedprice.subscription = price.subscription;
            updatedprice.payment = price.payment;
            db.SaveChanges();
            return RedirectToAction("price","admin");
        }


        [Authorize(Roles = "a")]
        public ActionResult updateprice1tb(int id)
        {
            var pr = db.pricing1tb.Find(id);
            return View(pr);
        }
        [Authorize(Roles = "a")]
        [HttpPost]
        public ActionResult updateprice1tb(pricing1tb price)
        {
            int id = price.id;

            var updatedprice = db.pricing1tb.Find(id);
            updatedprice.subscription = price.subscription;
            updatedprice.payment = price.payment;
            db.SaveChanges();
            return RedirectToAction("price", "admin");
        }


        [Authorize(Roles = "a")]
        public ActionResult updateprice4tb(int id)
        {
            var pr = db.pricing4tb.Find(id);
            return View(pr);
        }
        [Authorize(Roles = "a")]
        [HttpPost]
        public ActionResult updateprice4tb(pricing4tb price)
        {
            int id = price.id;

            var updatedprice = db.pricing4tb.Find(id);
            updatedprice.subscription = price.subscription;
            updatedprice.payment = price.payment;
            db.SaveChanges();
            return RedirectToAction("price", "admin");
        }
    }
}
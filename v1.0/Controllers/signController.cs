using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using v1._0.Models;
namespace v1._0.Controllers
{
    public class signController : Controller
    {
        FileArchivingEntities db = new FileArchivingEntities();
        // GET: sign

        public ActionResult signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signin(FormCollection form)
        {
            users model = new users();
            model.mail = form["mail"].Trim();
            model.password = form["password"].Trim();
            var userinDb = db.users.FirstOrDefault(x => x.mail == model.mail && x.password == model.password);
            var userinDb2 = db.adnnin.FirstOrDefault(x => x.mail == model.mail && x.password == model.password);
            if (userinDb != null)
            {
                FormsAuthentication.SetAuthCookie(userinDb.mail, false);
                Session["id"] = userinDb.id;
                return RedirectToAction("upload", "sign", userinDb);
            }
            else if (userinDb2 != null)
            {
                FormsAuthentication.SetAuthCookie(userinDb2.mail, false);
                Session["id"] = userinDb2.id;
                return RedirectToAction("admin", "admin");
            }
            else
            {
                ViewBag.mesaj = "geçersiz kullanıcı adı veya şifre";
                return View();
            }
        }

        public ActionResult signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("signin");
        }

        public ActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signup(FormCollection form)
        {
            users model = new users();
            var mail =form["mail"].ToString();
            var mailkontrol=db.users.FirstOrDefault(x => x.mail == mail);
            if (mailkontrol == null)
            {
                model.mail = form["mail"].Trim();
                model.password = form["password"].Trim();
                model.role = form["role"];
                db.users.Add(model);
                db.SaveChanges();
                return RedirectToAction("signin");
            }
            else
            {
                TempData["mailhata"] = "Bu mail ile daha önce üye olunmuş!";
            }

            return View();
        }

        [Authorize(Roles="u,p")]
        [HttpGet]
        public ActionResult upload()
        {
            var id = Session["id"];
            var y = Convert.ToInt32(id);
            var myQ1 = db.userfiles.Where(x => x.userid == y).ToList();

            string path = Server.MapPath("~/dosyalar/" + y + "/");
            Directory.CreateDirectory(path);

            DirectoryInfo dtinfo=new DirectoryInfo(Server.MapPath("~/dosyalar/" + y));
            var sizeOfdir = directorysize(dtinfo, true);
            long directorysize(DirectoryInfo dInfo, bool includeSubDir)
            {
                var totalsize = dInfo.EnumerateFiles().Sum(file => file.Length);
                if (includeSubDir)
                {
                    totalsize += dInfo.EnumerateDirectories().Sum(dir => directorysize(dir, true));
                }
                return totalsize;
            }

            double filesize= ((double)sizeOfdir) / (1024 * 1024);
            filesize = Math.Round(filesize, 2);
            ViewBag.fs = filesize;

            // kalan alan için dataya sütün aç kaç mb tanımladıysak o bilgiyi çek;
            var userinDb = db.users.FirstOrDefault(x => x.id == y && x.premium == null);
            if (userinDb != null)
            {
            ViewBag.kalanalan = 100 - filesize;
            ViewBag.hata = TempData["hata"];
            return View(myQ1);
            }
            userinDb = db.users.FirstOrDefault(x => x.id == y && x.premium == 250);
             if (userinDb != null)
            {
                ViewBag.kalanalan = 256000 - filesize;
                ViewBag.hata = TempData["hata"];
                return View(myQ1);
            }
            userinDb = db.users.FirstOrDefault(x => x.id == y && x.premium == 1024);
             if (userinDb != null)
            {
                ViewBag.kalanalan = 1048576 - filesize;
                ViewBag.hata = TempData["hata"];
                return View(myQ1);
            }
            userinDb = db.users.FirstOrDefault(x => x.id == y && x.premium == 4096);
            if (userinDb != null)
            {
                ViewBag.kalanalan = 4194304 - filesize;
                ViewBag.hata = TempData["hata"];
                return View(myQ1);
            }
            return View(myQ1);
        }

        [Authorize(Roles = "u,p")]
        [HttpPost]
        public ActionResult upload(FormCollection form)
        {
            userfiles model = new userfiles();
            model.userid = Convert.ToInt32(form["userid"]);

            if (Request.Files.Count > 0)
            {
                string dosyaadi1 = Path.GetFileName(Request.Files[0].FileName);
                //var deneme = db.userfiles.Where( x => x.name == dosyaadi1).ToList();
                var deneme = db.userfiles.FirstOrDefault(x => x.userid == model.userid && x.name == dosyaadi1);
                if (deneme == null )
                {
                    string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                    //string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string path = Server.MapPath("~/dosyalar/" + model.userid + "/");
                    Directory.CreateDirectory(path);
                    string yol = "~/dosyalar/" + model.userid + "/" + dosyaadi;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    model.filepath = "/dosyalar/" + model.userid + "/" + dosyaadi;
                    model.name = dosyaadi;
                    double x = ((double)(Request.Files[0].ContentLength) / 1048576);
                    x = Math.Round(x, 2);
                    model.size = x;
                    DateTime localDate = DateTime.Now;
                    model.date = localDate.Day.ToString()+"."+ localDate.Month.ToString()+"."+ localDate.Year.ToString();
                }
                else
                {

                    ViewBag.hata = "Aynı isimle bir dosya mevcut!!!";
                    TempData["hata"] = ViewBag.hata;
                    return RedirectToAction("upload", "sign");
                }
            }
            db.userfiles.Add(model);
            db.SaveChanges();
            return RedirectToAction("upload", "sign");

        }

        [Authorize(Roles = "u,p")]
        public ActionResult deletefiles(int id)
        {
            users userinDb = new users();
            var df = db.userfiles.Find(id);
            
            var gecici = db.userfiles.Find(id);
            db.userfiles.Remove(df);
            db.SaveChanges();
            userinDb.id = gecici.userid;
            if (System.IO.File.Exists(Server.MapPath("~/dosyalar/" + gecici.userid + "/" + gecici.name)))
            {
                System.IO.File.Delete(Server.MapPath("~/dosyalar/" + gecici.userid + "/" + gecici.name));
            }
            return RedirectToAction("upload", "sign", userinDb);
        }

        [Authorize(Roles = "u,p")]
        public FileResult download(userfiles uf, userfiles name)
        {
            var FileVirtualPath = "~/dosyalar/" + uf.id+"/"+ uf.name;
            return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));
        }
    }
}
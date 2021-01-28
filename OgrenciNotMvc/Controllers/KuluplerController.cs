using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class KuluplerController : Controller
    {
        // GET: Kulupler
        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();
        public ActionResult Index()
        {
            var kulupler = db.TBLKULUPLER.ToList();
            return View(kulupler);
        }
        [HttpGet]
        public ActionResult YeniKulup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKulup(TBLKULUPLER kulupp)
        {
            db.TBLKULUPLER.Add(kulupp);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            //TBLKULUPLER içerisinde bul id ye göre benim gönderdiğim değeri
            //id de View tarafında Sil değerine göre göndereceğimiz değer olacak
            //TBLKULUPLER içerisinden kaldır kulüp değişkeninden gelen değeri kaldırcak
            var kulup = db.TBLKULUPLER.Find(id);
            db.TBLKULUPLER.Remove(kulup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KulupGetir(int id)
        {
            var kulup = db.TBLKULUPLER.Find(id);
            return View("KulupGetir", kulup);
        }
        public ActionResult Guncelle(TBLKULUPLER kulupg)
        {
            //parametreden gelen kulübün ıd sini bul
            var kulup = db.TBLKULUPLER.Find(kulupg.KULUPID);
            kulup.KULUPAD = kulupg.KULUPAD;
            kulup.KULUPKONTENJAN = kulupg.KULUPKONTENJAN;
            db.SaveChanges();

            //Kulupler deki index e gönderiyoruz.
            return RedirectToAction("Index", "Kulupler");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    //DerslerController 
    public class DefaultController : Controller
    {
        //Burda ekleme,güncelleme,silme, listeleme kodlarını yazacağız..
        // burda yazmış olduğumuz kodları layout da ve view larda çağıracağız.

        // GET: Default
        //burda oluşturlan db nesnesi DbMvcOkulEntities içerisinde bulunan tablolara ulaşmamı sağlar.
        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();
        public ActionResult Index()
        {
            var dersler = db.TBLDERSLER.ToList();
            //sayfanın içerinde dersler adlı değişkenime gelicek olan değerleri döndür.
            return View(dersler);
        }
        [HttpGet]
        public ActionResult YeniDers()
        {
            return View();
        }

        //Değer göndereceğim tetikleme işlemi olduğunu belirtir.
        [HttpPost]
        public ActionResult YeniDers(TBLDERSLER dersp)
        {
            db.TBLDERSLER.Add(dersp);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var ders = db.TBLDERSLER.Find(id);
            db.TBLDERSLER.Remove(ders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DersGetir(int id)
        {
            //Dersler tablosunda bana id değerini bul
            var ders = db.TBLDERSLER.Find(id);
            return View("DersGetir",ders);
        }
        public ActionResult Guncelle(TBLDERSLER dersg)
        {
            var ders = db.TBLDERSLER.Find(dersg.DERSID);
            //Dışardan göndereceğimiz ismi veritabanındaki isme atadık.
            ders.DERSAD = dersg.DERSAD;
            db.SaveChanges();
            return RedirectToAction("Index", "Default");
        }
       
    }
}
    
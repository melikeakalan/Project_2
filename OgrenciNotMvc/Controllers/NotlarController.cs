using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using OgrenciNotMvc.Models;

namespace OgrenciNotMvc.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();
        public ActionResult Index()
        {
            var SinavNotlar = db.TBLNOTLAR.ToList();
            return View(SinavNotlar);
        }
        [HttpGet]
        public ActionResult YeniSinav()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSinav(TBLNOTLAR notp)
        {
            db.TBLNOTLAR.Add(notp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotGetir(int id)
        {
            var notlar = db.TBLNOTLAR.Find(id);
            return View("NotGetir", notlar);
        }
        //class ımı tanımladım notlardan bir nesne  türettim.sıfır yapmamızın nedeni; null bir ifade  gelirse sıkıntı çıkarmasın.
        // Değer göndereceğim bir tetikleme işlemi old.belirtir.
        [HttpPost]
        public ActionResult NotGetir(Class1 model, TBLNOTLAR notp, int SINAV1 = 0, int SINAV2 = 0, int SINAV3 = 0, int PROJE = 0)
        {
            //model içinde bulunan işlemim 
            if (model.Islem == "HESAPLA")
            {
                //işlem 1
                //ORTALAMA değişkeni oluşturdum bu parametrelerden değer aldım daha sonra ortalamayı view e taşıyacağız.
                int ORTALAMA = (SINAV1 + SINAV2 + SINAV3 + PROJE) / 4;
                ViewBag.ort = ORTALAMA;
            }
            if (model.Islem == "NOTGUNCELLE")
            {
                //önce TBLNOTLAR içerisinde bul,  notp parametresine göre NOTID yi bul. 
                var snv = db.TBLNOTLAR.Find(notp.NOTID);
                snv.SINAV1 = notp.SINAV1;
                snv.SINAV2 = notp.SINAV2;
                snv.SINAV3 = notp.SINAV3;
                snv.PROJE = notp.PROJE;
                snv.ORTALAMA = notp.ORTALAMA;
                db.SaveChanges();
                return RedirectToAction("Index", "Notlar");

            }
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();
        public ActionResult Index()
        {
            var ogrenciler = db.TBLOGRENCILER.ToList();
            return View(ogrenciler);
        }
        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            //en alttaki kulüp seçme dropdownlistteki  kullanıcıya gözükecek olan kısımda kulüplerin listelenmesini sağlıyoruz.
            //Burdaki LINQ sorgusunu view tarafında eşleştirip veritabanından kulüp verileri çekildi.
            List<SelectListItem> degerler = (from i in db.TBLKULUPLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();
            //burda oluşturulan dgr YeniOgrenci sayfasında kullanılacak
            //veritabanından veri çekildi..
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniOgrenci(TBLOGRENCILER ogrencip)
        {
            //LINQ sorgusuyla seçmiş olduğumuz kulübe ait ıd değerini atandı.
            //parametre ogrencip den gelen KULUPID değerine eşit olan değerin ilk çıkan 
            //değeri olan (firstordefault değerini) çekecek.
            //veri tabanına ilgili veriler eklendi..
            var klp = db.TBLKULUPLER.Where(m => m.KULUPID == ogrencip.TBLKULUPLER.KULUPID).FirstOrDefault();
            ogrencip.TBLKULUPLER = klp;
            db.TBLOGRENCILER.Add(ogrencip);
            db.SaveChanges();
            //Index e yönlendir..
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var ogrenci = db.TBLOGRENCILER.Find(id);
            db.TBLOGRENCILER.Remove(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Yeşil buton Güncelle ye basınca çalışır..
        public ActionResult OgrenciGetir(int id)
        {
            var ogrenci = db.TBLOGRENCILER.Find(id);
            return View("OgrenciGetir", ogrenci);
        }

        //Mavi buton güncelleye basınca çalışır..
        public ActionResult Guncelle(TBLOGRENCILER ogrencig)
        {
            var ogrenci = db.TBLOGRENCILER.Find(ogrencig.OGRENCIID);
            ogrenci.OGRAD = ogrencig.OGRAD;
            ogrenci.OGRSOYAD = ogrencig.OGRSOYAD;
            
            ogrenci.OGRCINSIYET = ogrencig.OGRCINSIYET;
            ogrenci.OGRKULUP = ogrencig.OGRKULUP;
            db.SaveChanges();
            //OgrenciController ı içerisinde bulunan İndex e git.
            return RedirectToAction("Index", "Ogrenci");
        }
    }
}
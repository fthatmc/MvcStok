using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Web.WebPages.Html;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
       DbMvcStokEntities1 db = new DbMvcStokEntities1 ();
        [Authorize]//giriş yapta şifresiz hatalı şifre girişini engelleme webconfig 20. satır
        public ActionResult Index(int sayfa=1)
        {
            //nuget package den PageList yükle yukarı ekle
            //var musteriliste = db.TblMusteri.ToList ();
            var musteriliste = db.TblMusteri.ToList().Where(x=>x.durum==true).ToList().ToPagedList(sayfa, 3);
            return View(musteriliste);
        }
        [HttpGet]
        public ActionResult MusteriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MusteriEkle(TblMusteri t)
        {
            if (!ModelState.IsValid)
            {
                return View("MusteriEkle");
                //REQUİRED VAR MODEL İÇİNDE PROPERTİYE YAZDIK
                //VİEW KISMINA DA KOD YAZILDI AYRICA BAK VALİDATİONMESSEGEFOR
            }
            t.durum = true;
            db.TblMusteri.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(TblMusteri p)
        {
            var musteribul = db.TblMusteri.Find(p.id);
            musteribul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TblMusteri.Find(id);
            return View("MusteriGetir", musteri);
        }
        public ActionResult MusteriGuncelle(TblMusteri m)
        {
            var musteri = db.TblMusteri.Find(m.id);
            musteri.ad = m.ad;
            musteri.soyad = m.soyad;
            musteri.sehir = m.sehir;
            musteri.bakiye = m.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
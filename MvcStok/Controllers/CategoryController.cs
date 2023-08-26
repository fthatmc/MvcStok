using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class CategoryController : Controller
    {
        
        DbMvcStokEntities1 db = new DbMvcStokEntities1();
        public ActionResult Index()
        {
            var Kategori= db.TblKategori.ToList();
            return View(Kategori);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(TblKategori p)
        {
            db.TblKategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var Kategori=db.TblKategori.Find(id);
            db.TblKategori.Remove(Kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult KategoriGetir(int id)
        {
            var Kategori = db.TblKategori.Find(id);
            return View("KategoriGetir",Kategori);
        }
        
        public ActionResult KategoriGuncelle(TblKategori k)
        {
            var Kategori = db.TblKategori.Find(k.id);
            Kategori.ad = k.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
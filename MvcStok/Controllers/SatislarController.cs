using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace MvcStok.Controllers
{
    public class SatislarController : Controller
    {
        DbMvcStokEntities1 db = new DbMvcStokEntities1 ();
        public ActionResult Index()
        {
            var satislar = db.TblSatışlar.ToList ();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult SatısEkle()
        {
            //DropDown Ürünler
            List<SelectListItem> urun = (from x in db.TblUrunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.urun = urun;

            //DropDown Personel
            List<SelectListItem> personel = (from x in db.TblPersonel.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad +" "+ x.soyad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.personel = personel;

            //DropDown Müşteri
            List<SelectListItem> musteri = (from x in db.TblMusteri.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad +" "+ x.soyad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.musteri = musteri;
            return View();
        }
        [HttpPost]
        public ActionResult SatısEkle(TblSatışlar s)
        {
            var urun = db.TblUrunler.Where(x=>x.id == s.TblUrunler.id).FirstOrDefault();
            var musteri = db.TblMusteri.Where(x=>x.id==s.TblMusteri.id).FirstOrDefault();
            var perspnel = db.TblPersonel.Where(x => x.id == s.TblPersonel.id).FirstOrDefault();
            s.TblUrunler = urun;
            s.TblMusteri = musteri;
            s.TblPersonel = perspnel;
            s.tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TblSatışlar.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
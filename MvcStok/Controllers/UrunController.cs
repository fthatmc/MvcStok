using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
       DbMvcStokEntities1 db = new DbMvcStokEntities1();
        public ActionResult Index(string p)
        {
            //var urun = db.TblUrunler.Where(x=>x.durum==true).ToList();
            //ürün arama
            var urun = db.TblUrunler.Where(x => x.durum == true);
            if (!string.IsNullOrEmpty(p)) //gönderilen parametre boş değilse
            {
                urun=urun.Where(x=>x.ad.Contains(p) && x.durum == true);
                //gönderilen parametreye ait değerleri getir ve durumu true olanları getir
            }
            return View(urun.ToList());
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            //dropdownlist kullanımı
            List<SelectListItem> kategori = (from x in db.TblKategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text=x.ad,
                                                 Value=x.id.ToString()
                                             }).ToList();
            ViewBag.drop=kategori;//controller dan view lere ViewBag ile veri taşınır
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TblUrunler t)
        {
            //var urun =db.TblUrunler.Add(t);
            //db.SaveChanges(urun);
            //return RedirectToAction("Index");
            //dropdownlist eklemeden yapsaydık böyle olacaktı
            t.durum = true;//durumu null alıyordu eklerken 
            var kategori = db.TblKategori.Where(x=>x.id==t.TblKategori.id).FirstOrDefault();
            t.TblKategori = kategori;
            
            db.TblUrunler.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(TblUrunler u)
        {
            var urun = db.TblUrunler.Find(u.id);
            //silme işlemi yapmış gibi yapacaz durumu false çevirip index te sadece durumu true olanları görecez
            // urunler tablosu kategori ile kategori id ilişkisinden dolayı mevcutsa eğer kategori ürünü silemezsin
            urun.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            //DROPDOWN LİST
            List<SelectListItem> kategori = (from x in db.TblKategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.ad,
                                                 Value = x.id.ToString()
                                             }).ToList();

            var urun = db.TblUrunler.Find(id);
            ViewBag.urunktgr= kategori;
            return View("UrunGetir", urun);
        }
        public ActionResult UrunGuncelle(TblUrunler k)
        {
            var urun = db.TblUrunler.Find(k.id);
            urun.ad = k.ad;
            urun.marka = k.marka;
            urun.stok = k.stok;
            urun.alisfiyat = k.alisfiyat;
            urun.satisfiyat = k.satisfiyat;
           
            var kategoridropdown = db.TblKategori.Where(x => x.id == k.TblKategori.id).FirstOrDefault();
            urun.kategori = kategoridropdown.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
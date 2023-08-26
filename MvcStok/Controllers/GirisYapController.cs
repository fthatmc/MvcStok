using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; //FormsAuthentication bunun için

namespace MvcStok.Controllers
{
    public class GirisYapController : Controller
    {
        DbMvcStokEntities1 db = new DbMvcStokEntities1 ();
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(TblAdmin a)
        {
            var bilgiler = db.TblAdmin.FirstOrDefault(x=>x.kullanici==a.kullanici && x.sifre==a.sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullanici, false);
                return RedirectToAction("Index","Musteri");   
            }
            else return View();

        }
    }
}
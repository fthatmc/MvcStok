using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class AdminController : Controller
    {
        DbMvcStokEntities1 db = new DbMvcStokEntities1 ();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TblAdmin a)
        {
            db.TblAdmin.Add (a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
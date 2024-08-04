using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using neizlesem_proje.Models;

namespace neizlesem_proje.Controllers
{
    
    public class HomeController : Controller
    {
        private neizlesem db = new neizlesem();

        public ActionResult Index(string mesaj)
        {
            ViewBag.uyari = mesaj;
            return View();
        }
        public ActionResult uye_giris()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> uye_giris(Uye uyem)
        {
            string mesaj = "";
            var uye = db.uyeler.FirstOrDefault(x => x.kul_adi == uyem.kul_adi && x.sifre == uyem.sifre);
            if (uye != null)
            {
                Session["uyem"] = uye;
                var uyenumarasi = uyem.uye_no;
                Session["uyenumarasi"] = uyenumarasi;
                if (uyem.kul_adi == "admin" && uyem.sifre == "administrator")
                {
                    Session["admin"] = "admin";
                    return RedirectToAction("Index2", "Film",new {mesaj});
                }
            }
            else
            {
                mesaj = "Kullanıcı adı veya şifre yanlış.";
                ViewBag.mesaj = mesaj;
                return View();
                
            }
            return RedirectToAction("Index","Film",new { mesaj });

        }

        public ActionResult guv_cik()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index","Film");
        }

        public ActionResult tur_doldur()
        {
            var turlerim = db.turler.ToList();
            return PartialView(turlerim);
        }

        public ActionResult sinema()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using neizlesem_proje.Models;
using PagedList;

namespace neizlesem_proje.Controllers
{

    public class FavoriController : Controller
    {
        private neizlesem db = new neizlesem();
        // GET: Favori
        public ActionResult Index(int? sayfa,int? uye_no)
        {
            var sayfa_no = sayfa ?? 1;
            uye_no = ((Uye)Session["uyem"]).uye_no;
            var favoriler = db.favoriler.Where(x=>x.uye_no==uye_no).ToList().ToPagedList(sayfa_no, 15);
            return View(favoriler);
        }
        public ActionResult favori_ekle(int id, int? uye_no)
        {
            try
            {
                if (Session["uyem"] == null)
                {
                    ViewBag.uyari = "Favorilere ekleyebilmek için giriş yapmalısınız.";
                    return RedirectToAction("Index", "Film");
                }
                else
                {
                    Favori favori = new Favori() { film_no = id, uye_no = ((Uye)Session["uyem"]).uye_no };
                    var favoriler = db.favoriler.Add(favori);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                ViewBag.uyari = "Bu film zaten favorilerinizde.";
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Favori favori = await db.favoriler.FindAsync(id);
            if (favori == null)
            {
                return HttpNotFound();
            }
            db.favoriler.Remove(favori);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
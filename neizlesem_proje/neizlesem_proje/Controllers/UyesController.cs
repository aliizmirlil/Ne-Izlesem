using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using neizlesem_proje.Models;

namespace neizlesem_proje.Controllers
{
    public class UyesController : Controller
    {
        private neizlesem db = new neizlesem();

        // GET: Uyes
        public async Task<ActionResult> Index()
        {
            return View(await db.uyeler.ToListAsync());
        }

        // GET: Uyes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = await db.uyeler.FindAsync(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }
        // GET: Uyes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Uyes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> Create([Bind(Include = "uye_no,ad_soyad,uye_mail,kul_adi,sifre")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.uyeler.Add(uye);
                    await db.SaveChangesAsync();
                    ViewBag.kayit_durum = " Kayıt başarılı. Giriş yapabilirsiniz.";
                }  
                catch (Exception hata)
                {
                    int yeri = hata.InnerException.ToString().IndexOf("uk_uyeler_uye_mail"); //-1 döndürürse kullanıcı adı hatasıdır
                    if (yeri != -1)
                    {
                        ViewBag.kayit_durum = "Bu e-mail sistemizde kayıtlıdır. Başka bir e-mail deneyiniz.";
                    }
                    else
                    {
                        ViewBag.kayit_durum = "Bu kullanıcı adı kullanımda. Başka bir kullanıcı adı deneyiniz.";
                        List<string> kuladi_onerilerim = kullanici_adi_oner(uye.kul_adi);
                        ViewBag.kuladi_onerilerim = kuladi_onerilerim;
                    }
                }
            }
            return View(uye);
        }

        // GET: Uyes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null || Session["uyem"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = await db.uyeler.FindAsync(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: Uyes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uye_no,ad_soyad,uye_mail,kul_adi,sifre")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(uye).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    ViewBag.durumu = "Bilgileriniz güncellendi.";
                    Session.RemoveAll();
                    Session.Abandon();
                    Task.WaitAll(Task.Delay(1000));
                    return RedirectToAction("uye_giris", "Home");
                }
                catch (Exception hata)
                {
                    int yeri = hata.InnerException.ToString().IndexOf("uk_uyeler_uye_mail");
                    if (yeri != -1)
                    {
                        ViewBag.durumu = "Bu e-mail kullanımda. Başka bir email deneyiniz.";
                    }
                    else
                    {
                        ViewBag.durumu = "Bu kullanıcı adı kullanımda. Başka bir kullanıcı adı deneyiniz.";
                    }
                }
            }
            return View(uye);
        }

        // GET: Uyes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = await db.uyeler.FindAsync(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            db.uyeler.Remove(uye);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        List<string> kuladi_oneri_listemiz = new List<string>();
        byte sayac = 0;
        List<string> kullanici_adi_oner(string kuladi)
        {
            System.Threading.Thread.Sleep(500); //random sistem saatine göre çalışır sistemi sleep ile yavaşlatmazsam random aynı şeyleri üretir !!
            string[] harfler = { "a", "b", "c", "d", "p", "r", "f" };
            string oneri_kuladi = kuladi + DateTime.Now.Year + harfler[new Random().Next(0, 4)];
            int mevcutmu = db.uyeler.Where(x => x.kul_adi == oneri_kuladi).Count();
            if (mevcutmu == 0)//dbde yok demektir
            {
                kuladi_oneri_listemiz.Add(oneri_kuladi);
                sayac++;
            }
            if (sayac != 3) kullanici_adi_oner(kuladi);
            return kuladi_oneri_listemiz;
        }
    }
}

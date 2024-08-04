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
    public class YonetmenController : Controller
    {
        private neizlesem db = new neizlesem();

        // GET: Yonetmen

        public async Task<ActionResult> Index()
        {
            return View(await db.yonetmenler.ToListAsync());
        }

        // GET: Yonetmen/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yonetmen yonetmen = await db.yonetmenler.FindAsync(id);
            if (yonetmen == null)
            {
                return HttpNotFound();
            }
            return View(yonetmen);
        }

        // GET: Yonetmen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yonetmen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "yonetmen_no,yonetmen_adsoyad")] Yonetmen yonetmen)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.yonetmenler.Add(yonetmen);
                    await db.SaveChangesAsync();
                    ViewBag.msj = "Yönetmen kaydedildi.";
                }
                catch (Exception hata)
                {
                    int yeri = hata.InnerException.ToString().IndexOf("IX_yonetmen_adsoyad");
                    if (yeri != 1)
                    {
                        ViewBag.msj = "Bu yönetmen zaten kayıtlı.";
                    }
                }

            }

            return View(yonetmen);
        }

        // GET: Yonetmen/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yonetmen yonetmen = await db.yonetmenler.FindAsync(id);
            if (yonetmen == null)
            {
                return HttpNotFound();
            }
            return View(yonetmen);
        }

        // POST: Yonetmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "yonetmen_no,yonetmen_adsoyad")] Yonetmen yonetmen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yonetmen).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(yonetmen);
        }

        // GET: Yonetmen/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yonetmen yonetmen = await db.yonetmenler.FindAsync(id);
            if (yonetmen == null)
            {
                return HttpNotFound();
            }
            db.yonetmenler.Remove(yonetmen);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Yonetmen/Delete/5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using neizlesem_proje.Models;
using PagedList;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace neizlesem_proje.Controllers
{
    public class FilmController : Controller
    {
        private neizlesem db = new neizlesem();
        // GET: Film
        public ActionResult Index(int? sayfa, int? id)
        {
            var sayfa_no = sayfa ?? 1;
            IEnumerable<Film> filmler = null;
            if (id == null)
            {

                filmler = db.filmler.ToList().ToPagedList(sayfa_no,15);
            }
            else
            filmler = db.filmler.ToList().Where(x => x.tur_no == id).ToPagedList(sayfa_no,15);
            ViewBag.filmler = filmler;
            return View(filmler);
        }

        public ActionResult Index2(int? sayfa, int? id)
        {
            var sayfa_no = sayfa ?? 1;
            IEnumerable<Film> filmler = null;
            if (id == null)
            {

                filmler = db.filmler.ToList().ToPagedList(sayfa_no, 15);
            }
            else
                filmler = db.filmler.ToList().Where(x => x.tur_no == id).ToPagedList(sayfa_no, 15);
            ViewBag.filmler = filmler;
            return View(filmler);
        }

        public ActionResult film_goster(int id)
        {
            var film = db.filmler.ToList().Where(x=>x.film_no==id);
            return View(film);
        }
        public ActionResult Create()
        {
            ViewBag.tur_no = new SelectList(db.turler, "tur_no", "tur_adi");
            ViewBag.yonetmen_no = new SelectList(db.yonetmenler, "yonetmen_no", "yonetmen_adsoyad");
            return View();
        }

        // POST: Filmsv23/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "film_no,film_adi,film_ozeti,tur_no,film_afis,film_cikis_tarih,puan,yonetmen_no,yildizlar")] Film film,HttpPostedFileBase file)
        {
            string dosya_adi = "stokfoto.png";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string uzanti = Path.GetExtension(file.FileName);
                    if (uzanti.Equals(".jpg") || uzanti.Equals(".png") || uzanti.Equals(".jpeg"))
                    {
                        dosya_adi = Path.GetFileName(file.FileName);
                        string yol = Path.Combine(Server.MapPath("~/film_afisleri"), dosya_adi);
                        file.SaveAs(yol);
                        ViewBag.msg = "Film kaydedildi.";
                    }
                    else
                    {
                        ViewBag.msg = "Resim formatı png,jpg veya jpeg olmalı.";
                    }
                }
                else
                {
                    ViewBag.msg = "Film stok resim ile kaydedildi.";
                }
            }
            film.film_afis = dosya_adi;
            if (ModelState.IsValid)
            {
                db.filmler.Add(film);
                await db.SaveChangesAsync();
                return RedirectToAction("Index2");
            }

            ViewBag.tur_no = new SelectList(db.turler, "tur_no", "tur_adi", film.tur_no);
            ViewBag.yonetmen_no = new SelectList(db.yonetmenler, "yonetmen_no", "yonetmen_adsoyad", film.yonetmen_no);
            return View(film);
        }

        // GET: Filmsv23/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = await db.filmler.FindAsync(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.tur_no = new SelectList(db.turler, "tur_no", "tur_adi", film.tur_no);
            ViewBag.yonetmen_no = new SelectList(db.yonetmenler, "yonetmen_no", "yonetmen_adsoyad", film.yonetmen_no);
            return View(film);
        }

        // POST: Filmsv23/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "film_no,film_adi,film_ozeti,tur_no,film_afis,film_cikis_tarih,puan,yonetmen_no,yildizlar")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index2");
            }
            ViewBag.tur_no = new SelectList(db.turler, "tur_no", "tur_adi", film.tur_no);
            ViewBag.yonetmen_no = new SelectList(db.yonetmenler, "yonetmen_no", "yonetmen_adsoyad", film.yonetmen_no);
            return View(film);
        }

        // GET: Filmsv23/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = await db.filmler.FindAsync(id);
            if (film != null)
            {
                db.filmler.Remove(film);
                await db.SaveChangesAsync();
                return RedirectToAction("Index2");
            }
            return View(film);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult film_oner()
        {
            alanlari_doldur();
            return View();
        }
        [HttpPost]

        /* onerilen filmler action u IEnumerable<Film> onerilen_film, int? sayfa */
        public ActionResult film_oner(double? puan, int? tur_no, int? film_cikis_tarih)
        {
            alanlari_doldur();
            var onerilen_films = db.filmler.Where(x => x.puan == puan && x.tur_no == tur_no && x.film_cikis_tarih.Year == film_cikis_tarih);
            return RedirectToAction("onerilen_filmler", new { puani = puan, turno=tur_no,cikistarih=film_cikis_tarih});

        }

        // buraya null geliyor onerilen_films coz
        public ActionResult onerilen_filmler(double? puani,int? turno,int? cikistarih,int? sayfa)
        {
            var sayfa_no = sayfa ?? 1;
            IEnumerable<Film> film = null;
            if (puani == 9)
            {
                if (cikistarih != 2015 && cikistarih != 2010 && cikistarih != 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 9 && x.puan >= 8 && x.tur_no == turno && x.film_cikis_tarih.Year == cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 9 && x.puan >= 8 && x.tur_no == turno && x.film_cikis_tarih.Year <= cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2015)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 9 && x.puan >= 8 && x.tur_no == turno && x.film_cikis_tarih.Year <= 2015 && x.film_cikis_tarih.Year >= 2010).ToPagedList(sayfa_no, 15);
                }
                else
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 9 && x.puan >= 8 && x.tur_no == turno && x.film_cikis_tarih.Year <= 2010 && x.film_cikis_tarih.Year >= 2000).ToPagedList(sayfa_no, 15);

                }
            
            }
            else if (puani == 8)
            {
                if (cikistarih != 2015 && cikistarih != 2010 && cikistarih != 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 8 && x.puan >= 7 && x.tur_no == turno && x.film_cikis_tarih.Year == cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 8 && x.puan >= 7 && x.tur_no == turno && x.film_cikis_tarih.Year <= cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2015)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 8 && x.puan >= 7 && x.tur_no == turno && x.film_cikis_tarih.Year <= 2015 && x.film_cikis_tarih.Year >= 2010).ToPagedList(sayfa_no, 15);
                }
                else
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 8 && x.puan >= 7 && x.tur_no == turno && x.film_cikis_tarih.Year <= 2010 && x.film_cikis_tarih.Year >= 2000).ToPagedList(sayfa_no, 15);

                }

            }
            else if (puani == 7)
            {
                if (cikistarih != 2015 && cikistarih != 2010 && cikistarih != 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 7 && x.puan >= 6 && x.tur_no == turno && x.film_cikis_tarih.Year == cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 7 && x.puan >= 6 && x.tur_no == turno && x.film_cikis_tarih.Year <= cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2015)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 7 && x.puan >= 6 && x.tur_no == turno && x.film_cikis_tarih.Year <= 2015 && x.film_cikis_tarih.Year >= 2010).ToPagedList(sayfa_no, 15);
                }
                else
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 7 && x.puan >= 6 && x.tur_no == turno && x.film_cikis_tarih.Year <= 2010 && x.film_cikis_tarih.Year >= 2000).ToPagedList(sayfa_no, 15);

                }

            }
            else if (puani == 6)
            {
                if (cikistarih != 2015 && cikistarih != 2010 && cikistarih != 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 6 && x.puan >= 5 && x.tur_no == turno && x.film_cikis_tarih.Year == cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= 6 && x.puan >= 5 && x.tur_no == turno && x.film_cikis_tarih.Year <= cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2015)
                {
                   film = db.filmler.ToList().Where(x => x.puan <= 6 && x.puan >= 5 && x.tur_no == turno && x.film_cikis_tarih.Year <= 2015 && x.film_cikis_tarih.Year >= 2010).ToPagedList(sayfa_no, 15);
                }
                else
                {
                  film = db.filmler.ToList().Where(x => x.puan <= 6 && x.puan >= 5 && x.tur_no == turno && x.film_cikis_tarih.Year <= 2010 && x.film_cikis_tarih.Year >= 2000).ToPagedList(sayfa_no, 15);

                }

            }
            else
            {
                if (cikistarih != 2015 && cikistarih != 2010 && cikistarih != 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= puani && x.tur_no == turno && x.film_cikis_tarih.Year == cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2000)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= puani && x.tur_no == turno && x.film_cikis_tarih.Year <= cikistarih).ToPagedList(sayfa_no, 15);
                }
                else if (cikistarih == 2015)
                {
                    film = db.filmler.ToList().Where(x => x.puan <= puani && x.tur_no == turno && x.film_cikis_tarih.Year <= 2015 && x.film_cikis_tarih.Year >= 2010).ToPagedList(sayfa_no, 15);
                }
                else
                {
                    film = db.filmler.ToList().Where(x => x.puan <= puani && x.tur_no == turno && x.film_cikis_tarih.Year <= 2010 && x.film_cikis_tarih.Year >= 2000).ToPagedList(sayfa_no, 15);

                }
            }
        
            return View(film);
        }

        public void alanlari_doldur()
        {
            ViewBag.tur_no = new SelectList(db.turler, "tur_no", "tur_adi");
            List<SelectListItem> puanlar = new List<SelectListItem>();
            puanlar.Add(new SelectListItem() { Text = "8-9 arası", Value = "9" });
            puanlar.Add(new SelectListItem() { Text = "7-8 arası", Value = "8" });
            puanlar.Add(new SelectListItem() { Text = "6-7 arası", Value = "7" });
            puanlar.Add(new SelectListItem() { Text = "5-6 arası", Value = "6" });
            puanlar.Add(new SelectListItem() { Text = "5 ve altı", Value = "5" });
            ViewBag.puan = puanlar;

            List<SelectListItem> film_cikis_tarih = new List<SelectListItem>();
            film_cikis_tarih.Add(new SelectListItem() { Text = "2022", Value = "2022" });
            film_cikis_tarih.Add(new SelectListItem() { Text = "2021", Value = "2021" });
            film_cikis_tarih.Add(new SelectListItem() { Text = "2020", Value = "2020" });
            film_cikis_tarih.Add(new SelectListItem() { Text = "2015-2010 arası", Value = "2015" });
            film_cikis_tarih.Add(new SelectListItem() { Text = "2010-2000 arası", Value = "2010" });
            film_cikis_tarih.Add(new SelectListItem() { Text = "2000 ve öncesi", Value = "2000" });
            ViewBag.film_cikis_tarih = film_cikis_tarih;

            List<SelectListItem> siralama = new List<SelectListItem>();
            siralama.Add(new SelectListItem() { Text = "IMDb puanına göre artan", Value = "1" });
            siralama.Add(new SelectListItem() { Text = "IMDb puanına göre azalan", Value = "2" });
            siralama.Add(new SelectListItem() { Text = "Yıla göre küçükten büyüğe", Value = "3" });
            siralama.Add(new SelectListItem() { Text = "Yıla göre büyükten küçüğe", Value = "4" });
            ViewBag.siralama = siralama;
        }
    }
}
   
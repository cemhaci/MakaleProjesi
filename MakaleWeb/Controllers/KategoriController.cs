using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makale_Entity;
using Makale_BLL;
using MakaleWeb.filters;

namespace MakaleWeb.Controllers
{
    [auth]
    public class KategoriController : Controller
    {
        KategoriYonet ky=new KategoriYonet();

        // GET: Kategori
        public ActionResult Index()
        {
            return View(ky.listele());
        }

        // GET: Kategori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.kategoribul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // GET: Kategori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kategori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Kategori kategori)
        {
            ModelState.Remove("Degistirenkullanici");

            if (ModelState.IsValid)
            {
                BusinessLayer_Sonuc<Kategori> sonuc=ky.KategoriEkle(kategori);
				if (sonuc.hatalar.Count > 0)
				{
                    sonuc.hatalar.ForEach(x=>ModelState.AddModelError("",x));
                    return View(kategori);
				}
               return RedirectToAction("Index");
            }

            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.kategoribul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Kategori kategori)
        {
            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
              BusinessLayer_Sonuc<Kategori> sonuc= ky.KategoriUpdate(kategori);
                if (sonuc.hatalar.Count > 0)
                {
                    sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(kategori);
                }
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.kategoribul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]   //antiforgerytoken ı html sayfasına da yazdık ve bu siteye yapılan saldıralrı önlemek için kullnılır
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = ky.kategoribul(id);
            BusinessLayer_Sonuc<Kategori> sonuc = ky.kategoriSil(kategori);
           
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

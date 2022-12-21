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

namespace MakaleWeb.Controllers
{
    public class KullaniciController : Controller
    {
       
        KullaniciYonet ky= new KullaniciYonet();
        // GET: Kullanici
        public ActionResult Index()
        {
            return View(ky.listele());   //listele yapcağımız zaman bizden migration istiyor çünkü databaseaccesslayer projemizde createonmodel ı kullnadığımızdan dolayı. oyüzden update-database -forse yazdığımızda o sorunu ortadan kaldırıyoruz
        }

        // GET: Kullanici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = ky.KullaniciBul(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // GET: Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                ky.KullaniciKaydet(kullanici);
                
                return RedirectToAction("Index");
            }

            return View(kullanici);
        }

        // GET: Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = ky.KullaniciBul(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // POST: Kullanici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                ky.kullaniciUpdate(kullanici);
                return RedirectToAction("Index");
            }
            return View(kullanici);
        }

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = ky.KullaniciBul(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // POST: Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ky.kullaniciSil(id);
         
            return RedirectToAction("Index");
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makale_BLL;
using Makale_Entity;

namespace MakaleWeb.Controllers
{
    public class NoteController : Controller
    {
        KategoriYonet ky= new KategoriYonet();
        NotYonet ny=new NotYonet();
        // GET: Note
        public ActionResult Index()
        {
            var notes= ny.listeleQueryable().Include(n => n.kategori);


            if (Session["login"] != null)
			{
                Kullanici kul = (Kullanici)Session["login"];
                notes = ny.listeleQueryable().Include(n => n.kategori).Where(x => x.kullanici.ID == kul.ID);  //include burada kategorilere join yapıyor.queryable da bir sql sorgusu oluşuyor böylelikle join ile kategorileri dahil  edebiliyoruz.fakat listele deseydik listele methodumuz direkt çalıştırcağından dolayı include ile kategorileri join edemiycektik.ve kullanıcıyıda dahil ettik çünkü notlarımı görmek istedğim de sadece kendi yazdığım notlarımı görmeliyim Kullanici kul=(Kullanici) Session["login"];
          
            }
           
            return View(notes.ToList());
        }
        public ActionResult begendiklerim()
		{
            LikeYonet ly=new LikeYonet();
            var notes = ny.listeleQueryable().Include(n => n.kategori);
            if (Session["login"] != null)
            {
                Kullanici kul = (Kullanici)Session["login"];
                notes = ly.listeleQuaryable().Include("Kullanici").Include("Note").Where(x=>x.kullanici.ID==kul.ID).Select(x=>x.not).Include(k=>k.kategori);   //select ile notları yazmasak bize sadece beğenileri getirecektir. ama biz notların beğenileriini görmek istiyoruz

            }
            return View("Index",notes.ToList());
        }

        // GET: Note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = ny.NotBul(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(ky.listele(), "ID", "Baslik");  //crate in viewinde dropdawn list var ve o dropdown listte kategorileri doldurmak için selectlist kullandık.
            return View();
        }

        // POST: Note/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Note note)
        {
            if (ModelState.IsValid)
            {
                ny.NotKaydet(note);

                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(ky.listele(), "ID", "Baslik", note.KategoriId);
            return View(note);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note =ny.NotBul(id.Value); //notbul null olabiliri o yüzden value dedik
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(ky.listele(), "ID", "Baslik", note.KategoriId);
            return View(note);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Note note)
        {
            if (ModelState.IsValid)
            {
               ny.NotUpdate(note);
                
                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(ky.listele(), "ID", "Baslik", note.KategoriId);
            return View(note);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = ny.NotBul(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = ny.NotBul(id);
            ny.NotSil(note);
           
            return RedirectToAction("Index");
        }

        
    }
}

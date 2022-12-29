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
using MakaleWeb.filters;
using MakaleWeb.Models;

namespace MakaleWeb.Controllers
{
 
    public class NoteController : Controller
    {
        KategoriYonet ky= new KategoriYonet();
        NotYonet ny=new NotYonet();
        LikeYonet ly = new LikeYonet();
        // GET: Note
        [auth]
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
           
            var notes = ny.listeleQueryable().Include(n => n.kategori);
            if (Session["login"] != null)
            {
                Kullanici kul = (Kullanici)Session["login"];
                notes = ly.listeleQuaryable().Include("Kullanici").Include("Note").Where(x=>x.kullanici.ID==kul.ID).Select(x=>x.not).Include(k=>k.kategori);   //select ile notları yazmasak bize sadece beğenileri getirecektir. ama biz notların beğenileriini görmek istiyoruz

            }
            return View("Index",notes.ToList());
        }
        [auth]
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
        [auth]
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(CacheHelper.kategoriler(), "ID", "Baslik");  //crate in viewinde dropdawn list var ve o dropdown listte kategorileri doldurmak için selectlist kullandık.  listele metodunun yerine cachelper ı kullandık
            return View();
        }

        // POST: Note/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Note note)
        {
            Kullanici kul=null;      //session yapmazsak oluşturduğumuz notun kullanıcı idsini göremiyor ve kullanıcıidyi null atadığından kaydaetme gerçekleşmiyor
            if (Session["login"] != null)
            {
                kul = (Kullanici)Session["login"];

            }
            note.kullanici=kul;

            ViewBag.KategoriId = new SelectList(ky.listele(), "ID", "Baslik", note.KategoriId);
    

            ModelState.Remove("DegistirenKullanici"); //bunu yapınca degistiren kullanıcıyı kontrol etmiyor boş mu dolu mu diye
           
            if (ModelState.IsValid)
            {
                BusinessLayer_Sonuc<Note> sonuc=ny.NotKaydet(note);
				if (sonuc.hatalar.Count > 0)
				{
                    sonuc.hatalar.ForEach(x=>ModelState.AddModelError("",x));
                    return View(note);
                }
                return RedirectToAction("Index");

            }
            return View(note);

        }

        // GET: Note/Edit/5
        [auth]
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
            ModelState.Remove("DegistirenKullanici");
            ViewBag.KategoriId = new SelectList(ky.listele(), "ID", "Baslik", note.KategoriId);
          
            
            if (ModelState.IsValid)
            {
                BusinessLayer_Sonuc<Note> sonuc=ny.NotUpdate(note);
				if (sonuc.hatalar.Count > 0)
				{
                    sonuc.hatalar.ForEach(x=>ModelState.AddModelError("",x));
                    return View(note);
				}
                
                return RedirectToAction("Index");
            }
         return View(note);
        }
        [auth]
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
            BusinessLayer_Sonuc<Note> sonuc= ny.NotSil(note);
            if (sonuc.hatalar.Count > 0)
            {
                sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                return View(note);
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult GetLikes(int[] id_dizi)
        {
            List<int> likenot = new List<int>();

            Kullanici kul = (Kullanici)Session["login"];

            if (kul != null)
                likenot = ly.Listele(x => x.kullanici.ID == kul.ID && id_dizi.Contains(x.not.ID)).Select(x => x.not.ID).ToList();

            //select not_id from begeni where kullanici_id=2 and not_id in (1,5,8,9,6)

            return Json(new { sonuc = likenot });
        }

        public ActionResult SetLike(int notid, bool like)
        {
            int sonuc = 0;
            Kullanici kul = (Kullanici)Session["login"];
            if(kul== null)
            {
                return Json(new { hata = true, res = -1});// beğenide sıfır gelebilir çakışmasın diye -1 veridk
            }
            Note not = ny.NotBul(notid);
            Like begen = ly.BegeniBul(notid, kul.ID);

            if (begen != null && like == false)
            {
                sonuc = ly.BegeniSil(begen);
            }
            else if (begen == null && like == true)
            {
                sonuc = ly.BegeniEkle(new Like()
                {
                    kullanici = kul,
                    not = not
                });
            }

            if (sonuc > 0)
            {
                if (like)
                {
                    not.BegeniSayisi++;
                }
                else
                {
                    not.BegeniSayisi--;
                }

               BusinessLayer_Sonuc<Note> notupdate = ny.NotUpdate(not);

                if (notupdate.hatalar.Count == 0)
                {
                    return Json(new { hata = false, res = not.BegeniSayisi });
                }
            }
          
            

            return Json(new { hata = true, res = not.BegeniSayisi });

        }


    }
}

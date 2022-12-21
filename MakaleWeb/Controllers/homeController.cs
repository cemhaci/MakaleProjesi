using Makale_BLL;
using Makale_Common;
using Makale_Entity;
using Makale_Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakaleWeb.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        KullaniciYonet ky=new KullaniciYonet();
         NotYonet ny=new NotYonet();
        public ActionResult Index()
        {
            test test1= new test();    
           // test1.InsertTest();
           //test1.UpdateTest();
           //test1.DeleteTest();
           //test1.YorumEkle();
          
            //return View(ny.listele().OrderByDescending(x=>x.DegistirmeTarihi).ToList()); // responsitory de extra metot oluşturmaddan da bunu çalıştırıp sıralayabilliriz.
               return View(ny.listeleQueryable().OrderByDescending(x=>x.DegistirmeTarihi).ToList());
        }
        public ActionResult kategori(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);  //hata kodu döndürcek
            }
          KategoriYonet ky=new KategoriYonet();
            Kategori kategori= ky.kategoribul(id.Value);

            if(kategori == null)
            {
                return HttpNotFound();
            }
            return View("Index",kategori.notes);  //kategoribul da bulduğumuz id leri getiriyor sadece ve index view ine yönlendiriyoruz
        }
        public ActionResult enbegenilenler()
        {
            return View("Index",ny.listele().OrderByDescending(x=>x.BegeniSayisi).ToList());
        }
        public ActionResult about()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
         public ActionResult Login(LoginModel model)
        {
             KullaniciYonet ky=new KullaniciYonet();
            if (ModelState.IsValid)  //model geçerli bir model ise
            {
                BusinessLayer_Sonuc<Kullanici> sonuc=ky.loginKontrol(model);
                if (sonuc.hatalar.Count > 0)
                {
                    sonuc.hatalar.ForEach(x=> ModelState.AddModelError("",x));
                    return View(model);
                }
                Session["login"]=sonuc.nesne;

                uygulama.kullaniciAd=model.KullaniciAd;
               return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
         public ActionResult Register(KayitModel model)
        {
            uygulama.kullaniciAd = model.kullaniciad;
            if (ModelState.IsValid)
            {
                
                BusinessLayer_Sonuc<Kullanici> sonuc=ky.kaydet(model);

                if (sonuc.hatalar.Count > 0)
                {
                   sonuc.hatalar.ForEach(x=>ModelState.AddModelError("",x));  //foreach ile dönüyor. x burada hatanın miktarını belirtiyor.ilk hatayı alıp yazdırıyor sonra ikinci hata vs
                    return View(model);  //eğer hata varsa sonucu o şekilde gönder
                }
                return RedirectToAction("kayitbasarili");
            }
            return View(model);
        }
     
        public ActionResult useractivate(Guid id)
        {
             BusinessLayer_Sonuc<Kullanici> sonuc=ky.ActivateUser(id);
            if (sonuc.hatalar.Count > 0)
            {
                TempData["hatalar"]=sonuc.hatalar;
                return RedirectToAction("useractivate_hata");
            }
            return View();
        }
        public ActionResult useractivate_hata()
        {
            List<string> hatalar=null;
            if (TempData["hatalar"] != null)
            {
                hatalar=(List<string>)TempData["hatalar"];
            }
            return View(hatalar);
        }
        public ActionResult kayitbasarili()
        {
            return View();
        }
        public ActionResult profilgoster()
        {
            Kullanici kul=(Kullanici) Session["login"];

            return View(kul);
        }
        public ActionResult profildegistir()
        {
          
            Kullanici kul=(Kullanici) Session["login"];
            return View(kul);
        }
        [HttpPost]
        public ActionResult profildegistir( Kullanici kul,HttpPostedFileBase profilresmi)
		{
            uygulama.kullaniciAd = kul.KullaniciAd;
            ModelState.Remove("DegistirenKullanici");
			if (ModelState.IsValid)
			{
                if (profilresmi != null && profilresmi.ContentType == "image/jpeg" || profilresmi.ContentType == "image/jpg" || profilresmi.ContentType == "image/png")
            {
                string dosyaAdi=$"user_{kul.ID}.{profilresmi.ContentType.Split('/')[1]}"; //"image/jpeg" split bunu böler sonra nereden bölceğimzi belirtiriz ve kaçıncı indexi alcağımızı söyleriz
                profilresmi.SaveAs(Server.MapPath($"~/image/{dosyaAdi}"));
                kul.profilresim=dosyaAdi;
            }
            BusinessLayer_Sonuc<Kullanici> sonuc= ky.kullaniciUpdate(kul);
            if (sonuc.hatalar.Count > 0)
            {
                sonuc.hatalar.ForEach(x=>ModelState.AddModelError("",x));
                return View(sonuc.nesne);
            }
            return RedirectToAction("profilgoster");
			}

            
            return View(kul);
        }
		public ActionResult profilsil()
		{
            Kullanici kul=Session["login"] as Kullanici;
            BusinessLayer_Sonuc<Kullanici> sonuc=ky.kullaniciSil(kul.ID);

			if (sonuc.hatalar.Count > 0)
			{
                //hatalar ekranda gödterilir
                sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                return RedirectToAction("profilgoster",sonuc.nesne);  //silinmedi var olan nesneyi tekrar gönder dedi
            }

            Session.Clear();
            return RedirectToAction("Index");
		}
	}
}
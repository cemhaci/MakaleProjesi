using Makale_BLL;
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
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}
using Makale_BLL;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakaleWeb.Controllers
{
    public class YorumController : Controller
    {
        YorumYonet yy = new YorumYonet();
        // GET: Yorum
        public ActionResult YorumGoster(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            NotYonet ny=new NotYonet();
            Note not=ny.NotBul(id.Value);
           
            return PartialView("_PartialPageYorum", not.yorumlar);
        }
        [HttpPost]
        public ActionResult edit (int? id,string text)
        {
            if(id== null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
           
            Yorum yorum=yy.YorumBul(id.Value);

            if(yorum == null)
            {
                return new HttpNotFoundResult();
            }
            yorum.Text=text;
            if (yy.yorumguncelle(yorum) > 0)
            {
                return Json(new {sonuc=true},JsonRequestBehavior.AllowGet);
            }
            return Json(new { sonuc = false }, JsonRequestBehavior.AllowGet);
        } 
        [HttpPost]
        public ActionResult delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            yy.delete(id);
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}
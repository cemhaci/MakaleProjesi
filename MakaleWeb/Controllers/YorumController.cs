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
    }
}
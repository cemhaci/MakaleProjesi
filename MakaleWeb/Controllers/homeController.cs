using Makale_BLL;
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
        public ActionResult Index()
        {
            test test1= new test();    
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakaleWeb.filters
{
    public class exc : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.TempData["error"]=filterContext.Exception;
            filterContext.ExceptionHandled=true;
            filterContext.Result=new RedirectResult("/home/ErorPage");
        }
    }
}
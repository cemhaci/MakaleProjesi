using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakaleWeb.filters
{
    public class authAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Kullanici kul=(Kullanici) filterContext.HttpContext.Session["login"];
            if(kul!=null&& kul.Admin == false)
            {
                filterContext.Result=new RedirectResult("/home/Index");
            }
        } 
    }
}
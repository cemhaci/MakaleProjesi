using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MakaleWeb.filters
{
    public class auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["login"] == null)
            {
                filterContext.Result =new RedirectResult("/home/login");
            }
        }
    }
}
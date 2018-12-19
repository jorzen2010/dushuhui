using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dushuhui.Controllers
{
    public class UcenterBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["uname"] == null)
            {
                filterContext.Result = Redirect("/Account/Login");
            }
        }
	}
}
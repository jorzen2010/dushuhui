using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DushuhuiDal;

namespace dushuhui.Controllers
{
    public class AdminBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["uname"] == null || Session["guanliyuan"] == null)
            {
                filterContext.Result = Redirect("/Account/Login");
                ViewBag.avatar = "/AdminLTE/dist/img/user2-160x160.jpg";
                ViewBag.uname = "未登录";
            }

            else
            {
                UnitOfWork unitofwork = new UnitOfWork();
                ViewBag.avatar = unitofwork.rensRepository.GetByID(int.Parse(Session["uid"].ToString())).RenAvatar;
                ViewBag.uname = unitofwork.rensRepository.GetByID(int.Parse(Session["uid"].ToString())).RenNickName;
            }
        }
    }
}
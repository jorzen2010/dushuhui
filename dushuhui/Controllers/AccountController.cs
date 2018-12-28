using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data;
using System.Collections;
using DushuhuiDal;
using Common;
using dushuhuiEntity;

namespace dushuhui.Controllers
{

   
    public class AccountController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            FormsAuthentication.SignOut();
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.msg = "对不起，您尚未登陆不允许访问！";
            return View();
        }

        
        // [AllowAnonymous]
        // public ActionResult Login()
        //{
        //    FormsAuthentication.SignOut();
        //    if (!string.IsNullOrEmpty(Request["returnUrl"]))
        //    {

        //        ViewBag.returnUrl = Request["returnUrl"].ToString();
        //        ViewBag.msg = "对不起，您尚未登陆不允许访问！";

        //    }

        //    return View();
        //}

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies["uname"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["uid"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Session["uid"] = null;
            System.Web.HttpContext.Current.Session["uname"] = null;
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            System.Web.HttpContext.Current.Session["uname"] = "";
            string username = fc["RenUserEmail"];
            string password = CommonTools.ToMd5(fc["RenPassword"].ToString());
            bool rememberme = fc["RememberMe"] == null ? false : true;
            string reurnUrl = fc["ReturnUrl"];


            var rens = unitOfWork.rensRepository.Get(filter: u => u.RenUserEmail == username && u.RenPassword == password);

           

            if (rens.Count() > 0)
            {
                Ren ren = rens.First();
                if (ren.Status)
                {
                   
                    HttpCookie cookie = new HttpCookie("uname");
                    cookie.Value = username;
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                    HttpCookie cookieid = new HttpCookie("uid");
                    cookieid.Value = ren.Id.ToString();
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookieid);

                    System.Web.HttpContext.Current.Session["uid"] = ren.Id.ToString();
                    System.Web.HttpContext.Current.Session["uname"] = ren.RenUserEmail;
                    System.Web.HttpContext.Current.Session["guanliyuan"] = "false";
                    System.Web.HttpContext.Current.Session["peiduren"] = "false";

                    if (ren.RenQuanxian.Contains("peiduren")&&ren.PeiduStatus=="success")
                    {
                        System.Web.HttpContext.Current.Session["peiduren"] = "true";
                    }
                    if (ren.RenQuanxian.Contains("guanliyuan"))
                    {
                        System.Web.HttpContext.Current.Session["guanliyuan"] = "true";
                    }

                    FormsAuthentication.SetAuthCookie(username, rememberme);

                    if (string.IsNullOrEmpty(reurnUrl))
                    {
                        return RedirectToAction("Ucenter", "Ucenter");
                    }
                    else
                    {
                        ViewBag.msg = "没有权限";
                        return Redirect(reurnUrl);
                    }

                }

                else
                {
                    ViewBag.msg = "此已经被禁用，不允许登陆";
                    return View();

                }

            }
            else
            {
                ViewBag.msg = "用户名或密码错误了";
                return View();

            }

        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Ren ren)
        {
            ren.Status = true;
            ren.RenQuanxian = "xueyuan";
            ren.CreateTime = System.DateTime.Now;
            ren.RenPassword = CommonTools.ToMd5(ren.RenPassword);
            ren.RenYijuhua = "本人很懒，什么也没留下";
            ren.RenInfo = "本人很懒，什么也没留下";
            ren.RenAvatar = "/Resource/img/avatar.jpg";
            if (ModelState.IsValid)
            {
                unitOfWork.rensRepository.Insert(ren);
                unitOfWork.Save();
                return RedirectToAction("Login", "Account");
            }

            return View(ren);
        }
        

    }
}
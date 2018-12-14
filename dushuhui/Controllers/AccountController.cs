using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
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
            ViewBag.ReturnUrl = returnUrl;
            return View();
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
            ren.RenQuanxian = "学员";
            ren.CreateTime = System.DateTime.Now;
            ren.RenPassword = CommonTools.EncryptToMD5(ren.RenPassword);
            if (ModelState.IsValid)
            {
                unitOfWork.rensRepository.Insert(ren);
                unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }

            return View(ren);
        }
        

    }
}
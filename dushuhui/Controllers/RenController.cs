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
    public class RenController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Ren/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Ren ren)
        {
            ren.CreateTime = System.DateTime.Now;
            ren.RenPassword = CommonTools.EncryptToMD5(ren.RenPassword);
            if (ModelState.IsValid)
            {
                unitOfWork.rensRepository.Insert(ren);
                unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }

            return View("/Account/Register", ren);
        }
	}
}
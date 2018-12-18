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
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Shudan()
        {
            return View();
        }
        public ActionResult Peiduren()
        {
            return View();
        }
        public ActionResult Dushubiji()
        {
            return View();
        }
        public ActionResult Chuangshirenshuo()
        {
            return View();
        }
        public ActionResult Xueyuanchengzhang()
        {
            return View();
        }
        public ActionResult Shu()
        {
            return View();
        }
        public ActionResult Ren()
        {
            return View();
        }
        public ActionResult Dushuying()
        {
            return View();
        }
        public ActionResult UCenter(int? page)
        {
            if (Session["uid"].ToString() != "" && Session["uid"] != null)
            {
                int uid = int.Parse(Session["uid"].ToString());
                Ren ren = unitOfWork.rensRepository.GetByID(uid);
                ViewData["user"] = ren;

                var yings = unitOfWork.yingsRepository.Get();



                Pager pager = new Pager();
                pager.table = "YingList";
                pager.strwhere = "1=1";
                pager.PageSize = 2;
                pager.PageNo = page ?? 1;
                pager.FieldKey = "Id";
                pager.FiledOrder = "Id desc";

                pager = CommonDal.GetPager(pager);
                IList<YingList> dataList = DataConvertHelper<YingList>.ConvertToModel(pager.EntityDataTable);
                var PageList = new StaticPagedList<YingList>(dataList, pager.PageNo, pager.PageSize, pager.Amount);

                ViewBag.Amount = pager.Amount;
                return View(PageList);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Daka()
        {
            return View();
        }
        public ActionResult UCDushuying()
        {
            return View();
        }
        public ActionResult PeiduDushuying()
        {
            return View();
        }

    }
}
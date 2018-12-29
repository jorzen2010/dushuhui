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
using DushuhuiService;

namespace dushuhui.Controllers
{
    public class UcenterController : UcenterBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
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
                pager.strwhere = "Dushuying=" + int.Parse(Session["uid"].ToString());
                pager.PageSize = 15;
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

        public ActionResult ShenqingPeiduren(int id)
        {
            if (Session["uid"].ToString() != "" && Session["uid"] != null)
            {
                int uid = int.Parse(Session["uid"].ToString());
                Ren ren = unitOfWork.rensRepository.GetByID(uid);
                ViewData["user"] = ren;
                Notice notice = unitOfWork.noticesRepository.GetByID(id);

                return View(notice);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult CreateDushuying()
        {
            BookService book = new BookService();
            ViewData["booklist"]=book.GetBookSelectList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateDushuying(Ying ying)
        {


            if (ModelState.IsValid)
            {
                unitOfWork.yingsRepository.Insert(ying);
                unitOfWork.Save();
                YingList yinglist = new YingList();
                yinglist.Canjiaren = ying.Peiduren;
                yinglist.Dushuying = ying.Id;
                yinglist.Status = "success";
                yinglist.Peiduren = ying.Peiduren;
                yinglist.Shumu = ying.Shumu;
                yinglist.ShenqingTime = DateTime.Now;
                yinglist.SuccessTime = DateTime.Now;
                unitOfWork.yinglistsRepository.Insert(yinglist);
                unitOfWork.Save();
                return RedirectToAction("Ucenter", "Ucenter");
            }

            BookService book = new BookService();
            ViewData["booklist"] = book.GetBookSelectList();

            return View(ying);
        }


        public ActionResult Daka(int yid)
        {
            Ying ying = unitOfWork.yingsRepository.GetByID(yid);
            ViewData["dakaying"] = ying;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Daka(Biji biji)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.bijisRepository.Insert(biji);
                unitOfWork.Save();
                return RedirectToAction("Ucenter", "Ucenter");
            }
            Ying ying = unitOfWork.yingsRepository.GetByID(biji.Dushuying);
            ViewData["dakaying"] = ying;
            return View(biji);
        }

        public ActionResult Biji(int? page,int rid)
        {
            Pager pager = new Pager();
            pager.table = "Biji";
            pager.strwhere = "BijiZuozhe="+rid;
            pager.PageSize = 15;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Biji> dataList = DataConvertHelper<Biji>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Biji>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }


        public ActionResult DakaContent(int bid)
        {
            if (Session["uid"].ToString() != "" && Session["uid"] != null)
            {
                int uid = int.Parse(Session["uid"].ToString());
                Ren ren = unitOfWork.rensRepository.GetByID(uid);
                ViewData["user"] = ren;
                Biji biji = unitOfWork.bijisRepository.GetByID(bid);

                return View(biji);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult UcenterInfo(int id)
        {
             Ren ren = unitOfWork.rensRepository.GetByID(id);
             ViewData["user"] = ren;

             return View("~/Views/Ucenter/_PartialUcenterInfo.cshtml");
        }

        public ActionResult YingList(int? page,int yid)
        {
            Pager pager = new Pager();
            pager.table = "YingList";
            pager.strwhere = "Dushuying=" + yid;
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<YingList> dataList = DataConvertHelper<YingList>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<YingList>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            ViewBag.yid = yid;
            return View(PageList);

        }

        public ActionResult AccountSetting() 
        {
            if (Session["uid"].ToString() != "" && Session["uid"] != null)
            {
                int uid = int.Parse(Session["uid"].ToString());
                Ren ren = unitOfWork.rensRepository.GetByID(uid);
              
                return View(ren);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AccountSetting(Ren ren)
        {


            if (ModelState.IsValid)
            {
                Ren nren = unitOfWork.rensRepository.GetByID(ren.Id);
                nren.RenNickName = ren.RenNickName;
                nren.RenName = ren.RenName;
                nren.RenSex = ren.RenSex;
                nren.RenBirthday = ren.RenBirthday;
                nren.RenYijuhua = ren.RenYijuhua;
                nren.RenInfo = ren.RenInfo;
                nren.RenAvatar = ren.RenAvatar;
                unitOfWork.rensRepository.Update(nren);
                unitOfWork.Save();
                return RedirectToAction("Ucenter", "Ucenter");
            }

            return View(ren);
        }


	}
}
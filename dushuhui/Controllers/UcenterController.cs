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

        public ActionResult ShenqingPeiduren(int id)
        {
            if (Session["uid"].ToString() != "" && Session["uid"] != null)
            {
                int uid = int.Parse(Session["uid"].ToString());
                Ren ren = unitOfWork.rensRepository.GetByID(uid);
                ViewData["user"] = ren;

                id = 2;
                Notice notice = unitOfWork.noticesRepository.GetByID(id);

                return View(notice);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult CreateDushuying(int id)
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
                return RedirectToAction("Ucenter", "Ucenter");
            }

            BookService book = new BookService();
            ViewData["booklist"] = book.GetBookSelectList();

            return View(ying);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.booksRepository.Insert(book);
                unitOfWork.Save();
                return RedirectToAction("Index", "Book");
            }

            return View(book);
        }

	}
}
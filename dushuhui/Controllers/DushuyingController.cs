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
    public class DushuyingController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Ying/
        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Ying";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Ying> dataList = DataConvertHelper<Ying>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Ying>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }

        public ActionResult CreateYing()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateYing(Ying ying)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.yingsRepository.Insert(ying);
                unitOfWork.Save();
                return RedirectToAction("Index", "Ying");
            }

            return View(ying);
        }

        public ActionResult Edit(int id)
        {

            Ying ying = unitOfWork.yingsRepository.GetByID(id);

            if (ying == null)
            {
                return HttpNotFound();
            }
            return View(ying);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Ying ying)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.yingsRepository.Update(ying);
                unitOfWork.Save();
                return RedirectToAction("Index", "Ying");
            }
            return View(ying);
        }

        //彻底删除
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int? id)
        {
            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            {

                unitOfWork.booksRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
	}
}
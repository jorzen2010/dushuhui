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
        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Ren";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Ren> dataList = DataConvertHelper<Ren>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Ren>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Shenqing(int? id)
        {
            

            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            {

                Ren ren = unitOfWork.rensRepository.GetByID(id);
                ren.RenQuanxian = ren.RenQuanxian + ",peiduren";
                ren.PeiduStatus = "shenqing";
                unitOfWork.rensRepository.Update(ren);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


       

	}
}
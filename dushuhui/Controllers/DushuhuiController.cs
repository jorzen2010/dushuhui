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
    public class DushuhuiController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Dushuhui/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookList(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Book";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Book> dataList = DataConvertHelper<Book>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Book>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }
        public ActionResult Setting()
        {
            int id = 1;
            Setting setting = unitOfWork.settingsRepository.GetByID(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }
        public ActionResult List()
        {
            return View();
        }
        
	}
}
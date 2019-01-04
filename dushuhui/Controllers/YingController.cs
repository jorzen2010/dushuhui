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
    public class YingController : AdminBaseController
    {
        //
        // GET: /Biji/
        public ActionResult Index(int? page,string type)
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

        public ActionResult Yinglist(int? page, int yid)
        {
            Pager pager = new Pager();
            pager.table = "YingList";
            pager.strwhere = " Dushuying="+yid;
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<YingList> dataList = DataConvertHelper<YingList>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<YingList>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }
	}
}
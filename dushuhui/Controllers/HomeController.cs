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
        
        public ActionResult Shudan(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Book";
            pager.strwhere = "1=1";
            pager.PageSize = 12;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Book> dataList = DataConvertHelper<Book>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Book>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
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
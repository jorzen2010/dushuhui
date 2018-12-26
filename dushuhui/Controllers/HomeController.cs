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

        public ActionResult Peiduren(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Ren";
            pager.strwhere = "1=1";
            pager.PageSize = 12;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Ren> dataList = DataConvertHelper<Ren>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Ren>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }
        public ActionResult Dushuying()
        {
            return View();
        }
        public ActionResult Chuangshirenshuo()
        {
            return View();
        }
        public ActionResult Xueyuanchengzhang(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Biji";
            pager.strwhere = "Tags like '%成长%'";
            pager.PageSize = 15;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Biji> dataList = DataConvertHelper<Biji>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Biji>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }
        public ActionResult Shu(int bid)
        {
            Book book = unitOfWork.booksRepository.GetByID(bid);
            return View(book);
        }
        public ActionResult Ren(int rid)
        {
            Ren ren = unitOfWork.rensRepository.GetByID(rid);
            return View(ren);
        }
        public ActionResult Dushubiji(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Biji";
            pager.strwhere = "1=1";
            pager.PageSize = 15;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Biji> dataList = DataConvertHelper<Biji>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Biji>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }
        public ActionResult BijiContent(int bid)
        {
            
                Biji biji = unitOfWork.bijisRepository.GetByID(bid);

                return View(biji);
           
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
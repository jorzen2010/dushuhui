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
                pager.strwhere = "Canjiaren=" + int.Parse(Session["uid"].ToString());
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
               
                var dianzan = unitOfWork.bijiDianzansRepository.Get(filter: u => u.DianzanBiji == bid && u.Dianzanren == ren.Id && u.Dianzan ==true);
                ViewBag.dianzan = false;
                if (dianzan.Count() > 0)
                {
                    ViewBag.dianzan = true;
                }
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

        public ActionResult PinglunList(int bid)
        {
            var Pingluns = unitOfWork.bijiPinglunsRepository.Get(filter: u => u.PinglunBiji == bid, orderBy: q => q.OrderByDescending(u => u.Id));
            ViewData["Pingluns"] = Pingluns;

            return View("~/Views/Ucenter/_PartialPinglun.cshtml");
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

        //喜欢笔记
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult XihuanBiji(int BijiZuozhe, int Canyuren, int Dushuying, int Peiduren, int Shumu, int Biji, bool status)
        {
            BijiDianzan xihuan = new BijiDianzan();
            xihuan.BijiZuozhe = BijiZuozhe;
            xihuan.Dianzan = status;
            xihuan.DianzanBiji = Biji;
            xihuan.Dianzanren = Canyuren;
            xihuan.Dushuying = Dushuying;
            xihuan.Peiduren = Peiduren;
            xihuan.Shumu = Shumu;
            Message msg = new Message();
            try
            {
                var dianzan = unitOfWork.bijiDianzansRepository.Get(filter: u => u.DianzanBiji == Biji && u.Dianzanren == Canyuren);

                if (dianzan.Count() > 0)
                {
                    BijiDianzan _xihuan = dianzan.First();
                    _xihuan.Dianzan = xihuan.Dianzan;
                    _xihuan.CreateTime = System.DateTime.Now;
                    unitOfWork.bijiDianzansRepository.Update(_xihuan);
                }
                else
                {
                    xihuan.CreateTime = System.DateTime.Now;
                    unitOfWork.bijiDianzansRepository.Insert(xihuan);
                }             
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "点赞成功";
            }
            catch {
                msg.MessageStatus = "false";
                msg.MessageInfo = "点赞失败";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //评论笔记
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PinglunBiji(int BijiZuozhe, int Canyuren, int Dushuying, int Peiduren, int Shumu, int Biji, string Pinglun)
        {
            BijiPinglun pinglun = new BijiPinglun();
            pinglun.BijiZuozhe = BijiZuozhe;
            pinglun.PinglunContent = Pinglun;
            pinglun.Pinglunren = Canyuren;
            pinglun.PinglunBiji = Biji;
            pinglun.Dushuying = Dushuying;
            pinglun.Peiduren = Peiduren;
            pinglun.Shumu = Shumu;
            pinglun.CreateTime = DateTime.Now;
            Message msg = new Message();
           
            try
            {

                unitOfWork.bijiPinglunsRepository.Insert(pinglun);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "评论成功";
            }
            catch
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "评论失败";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        //评论笔记
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult HuifuPinglun( string PinglunContent,int Pinglunren,int PinglunReplyren,int ReplyPinlun,int BijiZuozhe,int Peiduren,int Shumu,int PinglunBiji,int Dushuying)
        {
            PinglunReply huifupinglun = new PinglunReply();
            huifupinglun.BijiZuozhe = BijiZuozhe;
            huifupinglun.PinglunContent = PinglunContent;
            huifupinglun.Pinglunren = Pinglunren;
            huifupinglun.PinglunBiji = PinglunBiji;
            huifupinglun.Dushuying = Dushuying;
            huifupinglun.Peiduren = Peiduren;
            huifupinglun.Shumu = Shumu;
            huifupinglun.PinglunReplyren = PinglunReplyren;
            huifupinglun.ReplyPinlun = ReplyPinlun;           
            huifupinglun.CreateTime = DateTime.Now;
            Message msg = new Message();

            try
            {

                unitOfWork.pinglunReplysRepositorysRepository.Insert(huifupinglun);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "回复评论成功";
            }
            catch
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "回复评论失败";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


	}
}
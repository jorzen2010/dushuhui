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
    public class DushuhuiController : AdminBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Dushuhui/
       
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
      
        public ActionResult List()
        {
            return View();
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
        // POST: /Setting/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SettingEdit([Bind(Include = "Id,SiteName,DomainName,Logo,Copyright,Statistics,Protocol,Title,Keywords,Description,FileUploadUrl,EditorUploadUrl,ImgUploadUrl,AvatarUploadUrl,BaseUrl,WXAppID,WXAppSecret,WBAppID,WBAppSecret,QQAppID,QQAppSecret,MsgUserName,MsgPassword,MsgAPI,LockedMinutes,FailedPassword,CodeSeconds,CodeMinutes,EmailHost,EmailPort,EmailFrom,EmailUser,EmailPassword,ActiveMinutes,EmailCodeTitle,EmailCodeContent,EmailLinkTitle,EmailLinkContent,ResetLinkTitle,ResetLinkContent")] Setting setting)
        {
            if (ModelState.IsValid)
            {

                unitOfWork.settingsRepository.Update(setting);
                unitOfWork.Save();
                return RedirectToAction("Setting", "Dushuhui", new { msg = "success" });
            }
            return RedirectToAction("Setting", "Dushuhui", new { msg = "error" });
        }
        
	}
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dushuhuiEntity;
using DushuhuiDal;

namespace DushuhuiService
{
    public class PinglunService
    {

        public static List<PinglunReply> GetPinglunReplyByPinglunId(int pid)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            List<PinglunReply> pinglunReplys = unitOfWork.pinglunReplysRepositorys.Get(filter: u => u.ReplyPinlun == pid, orderBy: q => q.OrderByDescending(u => u.Id)) as List<PinglunReply>;

            return pinglunReplys;
        }

        public static bool  GetDianzanById(int pid,int uid)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            bool status = false;

            var  dzPingluns = unitOfWork.dianzanPinglunsRepository.Get(filter: u => u.DZPinglun == pid && u.Dianzanren == uid && u.Dianzan == true);
            if (dzPingluns.Count() > 0)
            {
                status = true;
            }


            return status;

        }

       
    }
}

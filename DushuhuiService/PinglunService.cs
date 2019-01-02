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

        public static int GetPinglunCount(int bid)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            int pingluncount = 0;
            var pinglun = unitOfWork.bijiPinglunsRepository.Get(filter:u =>u.PinglunBiji==bid);
            var pinglunreply = unitOfWork.pinglunReplysRepositorys.Get(filter:u =>u.PinglunBiji==bid);
            pingluncount = pinglun.Count() + pinglunreply.Count();
            return pingluncount;
        }

        public static int GetZanCount(int bid)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            int zancount = 0;
            var pinglunZan = unitOfWork.bijiDianzansRepository.Get(filter: u => u.DianzanBiji == bid);
            zancount = pinglunZan.Count();
            return zancount;
        }
    }
}

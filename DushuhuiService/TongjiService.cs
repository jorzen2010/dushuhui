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
    public class TongjiService
    {

        public static int GetBijiPinglunCount(int bid)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            int pingluncount = 0;
            var pinglun = unitOfWork.bijiPinglunsRepository.Get(filter:u =>u.PinglunBiji==bid);
            var pinglunreply = unitOfWork.pinglunReplysRepositorys.Get(filter:u =>u.PinglunBiji==bid);
            pingluncount = pinglun.Count() + pinglunreply.Count();
            return pingluncount;
        }

        public static int GetBijiZanCount(int bid)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            int zancount = 0;
            var pinglunZan = unitOfWork.bijiDianzansRepository.Get(filter: u => u.DianzanBiji == bid);
            zancount = pinglunZan.Count();
            return zancount;
        }

        public static int GetBijiZanCountByPeiduren(int pid)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            int zancount = 0;
            var pinglunZan = unitOfWork.bijiDianzansRepository.Get(filter: u => u.Peiduren == pid);
            zancount = pinglunZan.Count();
            return zancount;
        }

        public static int GetBijiPinglunCountByPeiduren(int pid)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            int pingluncount = 0;
            var pinglun = unitOfWork.bijiPinglunsRepository.Get(filter: u => u.Peiduren == pid);
            var pinglunreply = unitOfWork.pinglunReplysRepositorys.Get(filter: u => u.Peiduren == pid);
            pingluncount = pinglun.Count() + pinglunreply.Count();
            return pingluncount;
        }
    }
}

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
    public class YingService
    {
        

        public static  Ying  GetYingById(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Ying ying = unitOfWork.yingsRepository.GetByID(id);
            
            return ying;
        }
    }
}

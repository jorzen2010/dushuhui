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
    public class RenService
    {
        

        public static  Ren  GetRenById(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Ren ren = unitOfWork.rensRepository.GetByID(id);

            return ren;
        }
    }
}

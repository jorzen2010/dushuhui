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
    public class BookService
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<SelectListItem> GetBookSelectList()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var books = unitOfWork.booksRepository.Get();

            if (books.Count() > 0)
            {
                foreach (var book in books)
                {
                    SelectListItem Item = new SelectListItem { Text = book.BookName, Value = book.Id.ToString() };
                    items.Add(Item);           
                }
            }
            return items;
        }

        public static Ying GetYingById(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Ying ying = unitOfWork.yingsRepository.GetByID(id);

            return ying;
        }
    }
}

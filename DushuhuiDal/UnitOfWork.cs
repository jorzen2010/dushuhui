using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using dushuhuiEntity;


namespace DushuhuiDal
{
    public class UnitOfWork : IDisposable
    {
        private SkyWebContext context = new SkyWebContext();



         private GenericRepository<Setting> SettingsRepository;

         public GenericRepository<Setting> settingsRepository
         {
             get
             {

                 if (this.SettingsRepository == null)
                 {
                     this.SettingsRepository = new GenericRepository<Setting>(context);
                 }
                 return SettingsRepository;
             }
         }

         private GenericRepository<Book> BooksRepository;

         public GenericRepository<Book> booksRepository
         {
             get
             {

                 if (this.BooksRepository == null)
                 {
                     this.BooksRepository = new GenericRepository<Book>(context);
                 }
                 return BooksRepository;
             }
         }

         private GenericRepository<Ren> RensRepository;

         public GenericRepository<Ren> rensRepository
         {
             get
             {

                 if (this.RensRepository == null)
                 {
                     this.RensRepository = new GenericRepository<Ren>(context);
                 }
                 return RensRepository;
             }
         }

         private GenericRepository<Ying> YingsRepository;

         public GenericRepository<Ying> yingsRepository
         {
             get
             {

                 if (this.YingsRepository == null)
                 {
                     this.YingsRepository = new GenericRepository<Ying>(context);
                 }
                 return YingsRepository;
             }
         }


         private GenericRepository<YingList> YingListsRepository;

         public GenericRepository<YingList> yinglistsRepository
         {
             get
             {

                 if (this.YingListsRepository == null)
                 {
                     this.YingListsRepository = new GenericRepository<YingList>(context);
                 }
                 return YingListsRepository;
             }
         }

         private GenericRepository<Category> CategorysRepository;

         public GenericRepository<Category> categorysRepository
         {
             get
             {

                 if (this.CategorysRepository == null)
                 {
                     this.CategorysRepository = new GenericRepository<Category>(context);
                 }
                 return CategorysRepository;
             }
         }

         private GenericRepository<Notice> NoticesRepository;

         public GenericRepository<Notice> noticesRepository
         {
             get
             {

                 if (this.NoticesRepository == null)
                 {
                     this.NoticesRepository = new GenericRepository<Notice>(context);
                 }
                 return NoticesRepository;
             }
         }

        

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
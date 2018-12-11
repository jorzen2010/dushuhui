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
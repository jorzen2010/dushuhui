using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using dushuhuiEntity;


namespace DushuhuiDal
{
    public class SkyWebContext : DbContext
    {
        public SkyWebContext()
            : base("SkyWebContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Ren> Rens { get; set; }
        public DbSet<Biji> Bijis { get; set; }
        public DbSet<Ying> Yings { get; set; }
        public DbSet<YingList> YingLists { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Quanxian> Quanxians { get; set; }
        public DbSet<Notice> Notices { get; set; }




    }
}

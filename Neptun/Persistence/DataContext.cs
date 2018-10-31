using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Neptun.Models;

namespace Neptun.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("1GbDb")
        {
        }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Production> Productions { get; set; }
        public virtual DbSet<FeedBack> FeedBacks { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(x => x.IsRequired());
            modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(100));


            #region News

            modelBuilder.Entity<News>().HasKey(x => x.Id);
            modelBuilder.Entity<News>().Property(x => x.Description).HasMaxLength(50000);

            modelBuilder.Entity<News>().Property(x => x.Photo).HasMaxLength(200);
            modelBuilder.Entity<News>().Property(x => x.Photo).IsOptional();

            #endregion

            #region Company

            modelBuilder.Entity<Company>().HasKey(x => x.Id);

            #endregion

            #region Employee

            modelBuilder.Entity<Employee>().HasKey(x => x.Id);
            modelBuilder.Entity<Employee>().Property(x => x.Photo).IsOptional();
            modelBuilder.Entity<Employee>().Property(x => x.Photo).HasMaxLength(200);

            #endregion

            #region Feedback

            modelBuilder.Entity<FeedBack>().HasKey(x => x.Id);
            modelBuilder.Entity<FeedBack>().Property(x => x.Message).HasMaxLength(1000);
            modelBuilder.Entity<FeedBack>().Property(x => x.PhoneNuber).IsOptional();

            #endregion

            #region Production

            modelBuilder.Entity<Production>().HasKey(x => x.Id);
            modelBuilder.Entity<Production>().Property(x => x.Title).HasMaxLength(300);
            modelBuilder.Entity<Production>().Property(x => x.Description).HasMaxLength(50000);
            modelBuilder.Entity<Production>().Property(x => x.FullDescriptionPdf).HasMaxLength(200);
            modelBuilder.Entity<Production>().Property(x => x.FullDescriptionPdf).IsOptional();
            modelBuilder.Entity<Production>().Property(x => x.ButtonDescriptionName).IsOptional();
            modelBuilder.Entity<Production>().Property(x => x.Photo).HasMaxLength(200);
            modelBuilder.Entity<Production>().Property(x => x.Photo).IsOptional();

            modelBuilder.Entity<Production>().Property(x => x.PageTitle).IsOptional();
            modelBuilder.Entity<Production>().Property(x => x.PageDescription).IsOptional();
            modelBuilder.Entity<Production>().Property(x => x.PageKeywords).IsOptional();

            modelBuilder.Entity<Production>().Property(x => x.PageTitle).HasMaxLength(100);
            modelBuilder.Entity<Production>().Property(x => x.PageDescription).HasMaxLength(300);
            modelBuilder.Entity<Production>().Property(x => x.PageKeywords).HasMaxLength(300);

            #endregion
        }
    }
}
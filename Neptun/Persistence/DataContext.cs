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
            : base("DefaultConnection")
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
            modelBuilder.Entity<News>().Property(x => x.Description).HasMaxLength(10000);

            #endregion

            #region Company

            modelBuilder.Entity<Company>().HasKey(x => x.Id);

            #endregion

            #region Employee

            modelBuilder.Entity<Employee>().HasKey(x => x.Id);

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
            modelBuilder.Entity<Production>().Property(x => x.FullDescriptionPdf).IsOptional();
            modelBuilder.Entity<Production>().Property(x => x.ButtonDescriptionName).IsOptional();

            #endregion
        }
    }
}
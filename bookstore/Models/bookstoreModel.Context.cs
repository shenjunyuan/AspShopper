﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace bookstore.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bookstoreEntities : DbContext
    {
        public bookstoreEntities()
            : base("name=bookstoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<MemberTypes> MemberTypes { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<Programs> Programs { get; set; }
        public virtual DbSet<ProgramTypes> ProgramTypes { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Securitys> Securitys { get; set; }
        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<BigSales> BigSales { get; set; }
        public virtual DbSet<Categorys> Categorys { get; set; }
        public virtual DbSet<Featureds> Featureds { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Shippings> Shippings { get; set; }
        public virtual DbSet<Publishers> Publishers { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<OrdersDetail> OrdersDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<ApplicationBanner> ApplicationBanner { get; set; }
    }
}

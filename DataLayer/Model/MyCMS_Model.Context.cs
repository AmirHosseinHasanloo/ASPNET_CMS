﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EF_MyCMS_DBEntities : DbContext
    {
        public EF_MyCMS_DBEntities()
            : base("name=EF_MyCMS_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Page_Groups> Page_Groups { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
    }
}

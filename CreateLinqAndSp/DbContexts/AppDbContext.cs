using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CreateLinqAndSp.Models;

namespace CreateLinqAndSp.DbContexts
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblItem> TblItems { get; set; } = null!;
        public virtual DbSet<TblPartner> TblPartners { get; set; } = null!;
        public virtual DbSet<TblPartnerType> TblPartnerTypes { get; set; } = null!;
        public virtual DbSet<TblPurchase> TblPurchases { get; set; } = null!;
        public virtual DbSet<TblPurchaseDetail> TblPurchaseDetails { get; set; } = null!;
        public virtual DbSet<TblSale> TblSales { get; set; } = null!;
        public virtual DbSet<TblSalesDetail> TblSalesDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-N8EN9JO\\SQLEXPRESS;Database=TaskFromMahmudvai;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblItem>(entity =>
            {
                entity.HasKey(e => e.IntItemId)
                    .HasName("PK__tblItem__FA6F1B123D9D4BA9");
            });

            modelBuilder.Entity<TblPartner>(entity =>
            {
                entity.HasKey(e => e.IntPartnerId)
                    .HasName("PK__tblPartn__279F3038FA8FC6F3");
            });

            modelBuilder.Entity<TblPartnerType>(entity =>
            {
                entity.HasKey(e => e.IntPartnerTypeId)
                    .HasName("PK__tblPartn__353019536A6E1DCE");
            });

            modelBuilder.Entity<TblPurchase>(entity =>
            {
                entity.HasKey(e => e.IntPurchaseId)
                    .HasName("PK__tblPurch__39AFE6058A3D2FAE");
            });

            modelBuilder.Entity<TblPurchaseDetail>(entity =>
            {
                entity.HasKey(e => e.IntDetailsId)
                    .HasName("PK__tblPurch__0A1B5AF362E52B46");
            });

            modelBuilder.Entity<TblSale>(entity =>
            {
                entity.HasKey(e => e.IntSalesId)
                    .HasName("PK__tblSales__754F6C55A85CC1C6");
            });

            modelBuilder.Entity<TblSalesDetail>(entity =>
            {
                entity.HasKey(e => e.IntDetailsId)
                    .HasName("PK__tblSales__0A1B5AF32251E7AD");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

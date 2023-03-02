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

        public virtual DbSet<TblEcommerceOnlineDeliveryHeader> TblEcommerceOnlineDeliveryHeaders { get; set; } = null!;
        public virtual DbSet<TblEcommerceOnlineDeliveryRow> TblEcommerceOnlineDeliveryRows { get; set; } = null!;
        public virtual DbSet<TblItem> TblItems { get; set; } = null!;
        public virtual DbSet<TblPartner> TblPartners { get; set; } = null!;
        public virtual DbSet<TblPartnerType> TblPartnerTypes { get; set; } = null!;
        public virtual DbSet<TblPurchase> TblPurchases { get; set; } = null!;
        public virtual DbSet<TblPurchaseDetail> TblPurchaseDetails { get; set; } = null!;
        public virtual DbSet<TblSale> TblSales { get; set; } = null!;
        public virtual DbSet<TblSalesDetail> TblSalesDetails { get; set; } = null!;
        public virtual DbSet<TblVrmDailyPhysicalTestElementconfig> TblVrmDailyPhysicalTestElementconfigs { get; set; } = null!;
        public virtual DbSet<TblVrmDailyPhysicalTestHeader> TblVrmDailyPhysicalTestHeaders { get; set; } = null!;
        public virtual DbSet<TblVrmDailyPhysicalTestRow> TblVrmDailyPhysicalTestRows { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=TaskFromMahmudvai;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblEcommerceOnlineDeliveryHeader>(entity =>
            {
                entity.HasKey(e => e.IntDeliveryId)
                    .HasName("PK_tblECommerceOnlineDeliveryHeader_1");

                entity.Property(e => e.DteServerDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IntBillId).HasDefaultValueSql("((0))");

                entity.Property(e => e.IntJournalId).HasDefaultValueSql("((0))");

                entity.Property(e => e.IntReturnCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.IntSalesReferenceId).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCampainSales).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDirectSales).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOnline).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPrint).HasDefaultValueSql("((0))");

                entity.Property(e => e.NumBillAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.NumCardAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.NumCashAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.NumCreditAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.NumDueBillAmount).HasComputedColumnSql("(([numTotalDeliveryValue]-((((isnull([numCashAmount],(0))+isnull([numCardAmount],(0)))+isnull([numMFSAmount],(0)))+isnull([numSalesReturnAmount],(0)))+isnull([numRecoveryAmount],(0))))-isnull([numBillAmount],(0)))", false);

                entity.Property(e => e.NumPaymentAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.NumShippingCharge).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblEcommerceOnlineDeliveryRow>(entity =>
            {
                entity.HasKey(e => e.IntRowId)
                    .HasName("PK_tblDeliveryRow_1");

                entity.Property(e => e.IsNewSales).HasDefaultValueSql("((0))");

                entity.Property(e => e.NumReturnQuantity).HasDefaultValueSql("((0))");
            });

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

            modelBuilder.Entity<TblVrmDailyPhysicalTestElementconfig>(entity =>
            {
                entity.HasKey(e => e.IntTestElementId)
                    .HasName("PK__tblVrmDa__BAE2CD1F870AEE49");
            });

            modelBuilder.Entity<TblVrmDailyPhysicalTestHeader>(entity =>
            {
                entity.HasKey(e => e.IntDailyPhysicalTestId)
                    .HasName("PK__tblVrmDa__73E9C5D090444ECD");
            });

            modelBuilder.Entity<TblVrmDailyPhysicalTestRow>(entity =>
            {
                entity.HasKey(e => e.IntRowId)
                    .HasName("PK__tblVrmDa__6A2A8C9C506156C8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

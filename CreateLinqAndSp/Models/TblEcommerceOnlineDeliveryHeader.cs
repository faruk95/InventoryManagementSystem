using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblECommerceOnlineDeliveryHeader")]
    public partial class TblEcommerceOnlineDeliveryHeader
    {
        [Key]
        [Column("intDeliveryId")]
        public long IntDeliveryId { get; set; }
        [Column("strDeliveryCode")]
        [StringLength(50)]
        public string StrDeliveryCode { get; set; } = null!;
        [Column("intAccountId")]
        public long IntAccountId { get; set; }
        [Column("strAccountCode")]
        [StringLength(50)]
        public string StrAccountCode { get; set; } = null!;
        [Column("strAccountName")]
        [StringLength(100)]
        public string StrAccountName { get; set; } = null!;
        [Column("intBusinessUnitId")]
        public long IntBusinessUnitId { get; set; }
        [Column("strBusinessUnitCode")]
        [StringLength(50)]
        public string StrBusinessUnitCode { get; set; } = null!;
        [Column("strBusinessUnitName")]
        [StringLength(100)]
        public string StrBusinessUnitName { get; set; } = null!;
        [Column("strBusinessUnitAddress")]
        [StringLength(300)]
        public string StrBusinessUnitAddress { get; set; } = null!;
        [Column("intBusinessAreaId")]
        public long IntBusinessAreaId { get; set; }
        [Column("strBusinessAreaCode")]
        [StringLength(50)]
        public string StrBusinessAreaCode { get; set; } = null!;
        [Column("strBusinessAreaName")]
        [StringLength(100)]
        public string StrBusinessAreaName { get; set; } = null!;
        [Column("intSalesOrganizationId")]
        public long IntSalesOrganizationId { get; set; }
        [Column("strSalesOrganizationCode")]
        [StringLength(50)]
        public string StrSalesOrganizationCode { get; set; } = null!;
        [Column("strSalesOrganizationName")]
        [StringLength(100)]
        public string StrSalesOrganizationName { get; set; } = null!;
        [Column("intDistributionChannelId")]
        public long IntDistributionChannelId { get; set; }
        [Column("strDistributionChannelName")]
        [StringLength(100)]
        public string StrDistributionChannelName { get; set; } = null!;
        [Column("intDeliveryTypeId")]
        public long IntDeliveryTypeId { get; set; }
        [Column("strDeliveryTypeName")]
        [StringLength(100)]
        public string StrDeliveryTypeName { get; set; } = null!;
        [Column("intShipToPartnerId")]
        public long IntShipToPartnerId { get; set; }
        [Column("strShipToPartnerName")]
        [StringLength(100)]
        public string StrShipToPartnerName { get; set; } = null!;
        [Column("intSoldToPartnerId")]
        public long IntSoldToPartnerId { get; set; }
        [Column("strSoldToPartnerName")]
        [StringLength(100)]
        public string StrSoldToPartnerName { get; set; } = null!;
        [Column("strSoldToPartnerCode")]
        [StringLength(500)]
        public string StrSoldToPartnerCode { get; set; } = null!;
        [Column("strShipToPartnerAddress")]
        [StringLength(300)]
        public string StrShipToPartnerAddress { get; set; } = null!;
        [Column("intTransportZoneId")]
        public long IntTransportZoneId { get; set; }
        [Column("strTransportZoneName")]
        [StringLength(100)]
        public string StrTransportZoneName { get; set; } = null!;
        [Column("dteDeliveryDate", TypeName = "date")]
        public DateTime DteDeliveryDate { get; set; }
        [Column("intShipPointId")]
        public long IntShipPointId { get; set; }
        [Column("strShipPointName")]
        [StringLength(100)]
        public string StrShipPointName { get; set; } = null!;
        [Column("strAddress")]
        [StringLength(300)]
        public string StrAddress { get; set; } = null!;
        [Column("intPlantId")]
        public long? IntPlantId { get; set; }
        [Column("strPlantName")]
        [StringLength(200)]
        public string? StrPlantName { get; set; }
        [Column("intNumberOfTotalItem")]
        public long IntNumberOfTotalItem { get; set; }
        [Column("isPickingECommerceOnlineted")]
        public bool IsPickingEcommerceOnlineted { get; set; }
        [Column("isPackingECommerceOnlineted")]
        public bool IsPackingEcommerceOnlineted { get; set; }
        [Column("isShipmentECommerceOnlineted")]
        public bool IsShipmentEcommerceOnlineted { get; set; }
        [Column("isInventoryECommerceOnlineted")]
        public bool IsInventoryEcommerceOnlineted { get; set; }
        [Column("isBillingECommerceOnlineted")]
        public bool IsBillingEcommerceOnlineted { get; set; }
        [Column("isTax")]
        public bool? IsTax { get; set; }
        [Column("numTotalDeliveryQuantity", TypeName = "numeric(18, 6)")]
        public decimal NumTotalDeliveryQuantity { get; set; }
        [Column("numTotalDeliveryValue", TypeName = "numeric(18, 6)")]
        public decimal NumTotalDeliveryValue { get; set; }
        [Column("numHeaderDiscountValue", TypeName = "numeric(18, 6)")]
        public decimal NumHeaderDiscountValue { get; set; }
        [Column("numGrossDiscount", TypeName = "numeric(18, 6)")]
        public decimal NumGrossDiscount { get; set; }
        [Column("numTotalNetValue", TypeName = "numeric(18, 6)")]
        public decimal NumTotalNetValue { get; set; }
        [Column("strSellerName")]
        [StringLength(100)]
        public string StrSellerName { get; set; } = null!;
        [Column("strSellerAddress")]
        [StringLength(300)]
        public string StrSellerAddress { get; set; } = null!;
        [Column("strVatChallanNo")]
        [StringLength(50)]
        public string StrVatChallanNo { get; set; } = null!;
        [Column("intWarehouseId")]
        public long IntWarehouseId { get; set; }
        [Column("strWarehouseName")]
        [StringLength(100)]
        public string StrWarehouseName { get; set; } = null!;
        [Column("strWarehouseAddress")]
        [StringLength(300)]
        public string StrWarehouseAddress { get; set; } = null!;
        [Column("intInventoryPGIJVId")]
        public long? IntInventoryPgijvid { get; set; }
        [Column("intInventoryCOGSJVId")]
        public long? IntInventoryCogsjvid { get; set; }
        [Column("intSalesJVId")]
        public long? IntSalesJvid { get; set; }
        [Column("isRtmChallanReceive")]
        public bool? IsRtmChallanReceive { get; set; }
        [Column("intActionBy")]
        public long IntActionBy { get; set; }
        [Column("dteLastActionDateTime", TypeName = "datetime")]
        public DateTime DteLastActionDateTime { get; set; }
        [Column("dteServerDateTime", TypeName = "datetime")]
        public DateTime DteServerDateTime { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
        [Column("dteCollectionDate", TypeName = "date")]
        public DateTime? DteCollectionDate { get; set; }
        [Column("isPaymentReceived")]
        public bool? IsPaymentReceived { get; set; }
        [Column("numPaymentAmount", TypeName = "decimal(18, 4)")]
        public decimal? NumPaymentAmount { get; set; }
        [Column("dtePaymentDate", TypeName = "date")]
        public DateTime? DtePaymentDate { get; set; }
        [Column("isCashPayment")]
        public bool? IsCashPayment { get; set; }
        [Column("numCashAmount", TypeName = "decimal(18, 4)")]
        public decimal? NumCashAmount { get; set; }
        [Column("numCreditAmount", TypeName = "decimal(18, 4)")]
        public decimal? NumCreditAmount { get; set; }
        [Column("numMFSAmount", TypeName = "numeric(18, 6)")]
        public decimal? NumMfsamount { get; set; }
        [Column("numCashReturn", TypeName = "numeric(18, 4)")]
        public decimal? NumCashReturn { get; set; }
        [Column("strCardNo")]
        [StringLength(50)]
        public string? StrCardNo { get; set; }
        [Column("numShippingCharge", TypeName = "decimal(18, 4)")]
        public decimal? NumShippingCharge { get; set; }
        [Column("intBankId")]
        public long? IntBankId { get; set; }
        [Column("strBankName")]
        [StringLength(100)]
        public string? StrBankName { get; set; }
        [Column("numPoint", TypeName = "numeric(18, 4)")]
        public decimal? NumPoint { get; set; }
        [Column("isHold")]
        public bool? IsHold { get; set; }
        [Column("numCardAmount", TypeName = "numeric(18, 4)")]
        public decimal? NumCardAmount { get; set; }
        [Column("intPaymentMethodId")]
        public long? IntPaymentMethodId { get; set; }
        [Column("strPaymentMethodName")]
        [StringLength(50)]
        public string? StrPaymentMethodName { get; set; }
        [Column("intSalesReferenceId")]
        public long? IntSalesReferenceId { get; set; }
        [Column("isOnline")]
        public bool? IsOnline { get; set; }
        [Column("intJournalId")]
        public long? IntJournalId { get; set; }
        [Column("strJournalCode")]
        [StringLength(150)]
        public string? StrJournalCode { get; set; }
        [Column("isPrint")]
        public bool? IsPrint { get; set; }
        [Column("numMFSCommission", TypeName = "numeric(18, 6)")]
        public decimal? NumMfscommission { get; set; }
        [Column("numBankCommission", TypeName = "numeric(18, 6)")]
        public decimal? NumBankCommission { get; set; }
        [Column("isDirectSales")]
        public bool? IsDirectSales { get; set; }
        [Column("numFractionAmount", TypeName = "numeric(18, 6)")]
        public decimal? NumFractionAmount { get; set; }
        [Column("numSalesReturnAmount", TypeName = "numeric(18, 6)")]
        public decimal? NumSalesReturnAmount { get; set; }
        [Column("numRecoveryAmount", TypeName = "numeric(18, 6)")]
        public decimal? NumRecoveryAmount { get; set; }
        [Column("isCampainSales")]
        public bool? IsCampainSales { get; set; }
        [Column("intBillId")]
        public long? IntBillId { get; set; }
        [Column("numBillAmount", TypeName = "numeric(18, 6)")]
        public decimal? NumBillAmount { get; set; }
        [Column("intReturnCount")]
        public long? IntReturnCount { get; set; }
        [Column("numBillReceiveAmount", TypeName = "numeric(18, 6)")]
        public decimal? NumBillReceiveAmount { get; set; }
        [Column("numDueBillAmount", TypeName = "numeric(26, 6)")]
        public decimal? NumDueBillAmount { get; set; }
        [Column("status")]
        public int? Status { get; set; }
    }
}

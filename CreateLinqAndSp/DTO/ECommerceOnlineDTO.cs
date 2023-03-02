using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CreateLinqAndSp.DTO
{
    public class ECommerceOnlineDTO
    {
        public EcommerceOnlineDeliveryHeaderDTO Header { get; set; }
        public List<EcommerceOnlineDeliveryRowDTO?> Row { get; set; }
    }
    public class EcommerceOnlineDeliveryHeaderDTO
    {
     
        public long IntDeliveryId { get; set; }
       
        public string StrDeliveryCode { get; set; } = null!;
  
        public long IntAccountId { get; set; }
        
        public string StrAccountCode { get; set; } = null!;
       
        public string StrAccountName { get; set; } = null!;
    
        public long IntBusinessUnitId { get; set; }
  
        public string StrBusinessUnitCode { get; set; } = null!;
  
        public string StrBusinessUnitName { get; set; } = null!;
       
        public string StrBusinessUnitAddress { get; set; } = null!;

        public long IntBusinessAreaId { get; set; }

        public string StrBusinessAreaCode { get; set; } = null!;
 
        public string StrBusinessAreaName { get; set; } = null!;
           public long IntSalesOrganizationId { get; set; }

        public string StrSalesOrganizationCode { get; set; } = null!;
        public string StrSalesOrganizationName { get; set; } = null!;
        public long IntDistributionChannelId { get; set; }

        public string StrDistributionChannelName { get; set; } = null!;

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
        public List<EcommerceOnlineDeliveryRowDTO> Row { get; set; }
    }
    public class EcommerceOnlineDeliveryRowDTO
    {
        
        public long IntRowId { get; set; }

        public long IntDeliveryId { get; set; }

        public string StrDeliveryCode { get; set; } = null!;
        
        public long IntSalesOrderId { get; set; }
       
        public string StrSalesOrderCode { get; set; } = null!;
       
        public long IntSalesOrderRowId { get; set; }
      
        public long IntSalesOrderRowSequenceNo { get; set; }

        public long IntItemId { get; set; }

        public string StrItemSalesCode { get; set; } = null!;

        public string StrItemSalesName { get; set; } = null!;

        public string StrItemCode { get; set; } = null!;
        public string StrItemName { get; set; } = null!;
        public long IntUom { get; set; }
          public string StrUom { get; set; } = null!;

        public decimal NumQuantity { get; set; }

        public decimal NumItemPrice { get; set; }

        public decimal NumDeliveryValue { get; set; }

        public decimal NumTotalDiscountValue { get; set; }

        public decimal NumTotalShipingValue { get; set; }

        public decimal NumTotalTax { get; set; }

        public decimal NumNetValue { get; set; }

        public long IntLocationId { get; set; }

        public string StrLocationName { get; set; } = null!;

        public string? StrSpecification { get; set; }

        public bool IsFreeItem { get; set; }

        public bool IsActive { get; set; }

        public string? StrShipToPartnerContactNo { get; set; }

        public decimal NumMrp { get; set; }

        public decimal NumCogs { get; set; }

        public long? IntReferenceId { get; set; }

        public long? IntDlvrowId { get; set; }
       
        public long? IntParentDlvhid { get; set; }
        public bool? IsSalesReturn { get; set; }
        [Column("numReturnQuantity", TypeName = "numeric(18, 6)")]
        public decimal? NumReturnQuantity { get; set; }
        [Column("numMRPDiscount", TypeName = "numeric(18, 6)")]
        public decimal? NumMrpdiscount { get; set; }
        [Column("intRecordForSalesId")]
        public long? IntRecordForSalesId { get; set; }
        [Column("isNewSales")]
        public bool? IsNewSales { get; set; }
    }


}

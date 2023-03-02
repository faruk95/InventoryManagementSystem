using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblECommerceOnlineDeliveryRow")]
    public partial class TblEcommerceOnlineDeliveryRow
    {
        [Key]
        [Column("intRowId")]
        public long IntRowId { get; set; }
        [Column("intDeliveryId")]
        public long IntDeliveryId { get; set; }
        [Column("strDeliveryCode")]
        [StringLength(50)]
        public string StrDeliveryCode { get; set; } = null!;
        [Column("intSalesOrderId")]
        public long IntSalesOrderId { get; set; }
        [Column("strSalesOrderCode")]
        [StringLength(50)]
        public string StrSalesOrderCode { get; set; } = null!;
        [Column("intSalesOrderRowId")]
        public long IntSalesOrderRowId { get; set; }
        [Column("intSalesOrderRowSequenceNo")]
        public long IntSalesOrderRowSequenceNo { get; set; }
        [Column("intItemId")]
        public long IntItemId { get; set; }
        [Column("strItemSalesCode")]
        [StringLength(50)]
        public string StrItemSalesCode { get; set; } = null!;
        [Column("strItemSalesName")]
        [StringLength(300)]
        public string StrItemSalesName { get; set; } = null!;
        [Column("strItemCode")]
        [StringLength(50)]
        public string StrItemCode { get; set; } = null!;
        [Column("strItemName")]
        [StringLength(100)]
        public string StrItemName { get; set; } = null!;
        [Column("intUOM")]
        public long IntUom { get; set; }
        [Column("strUOM")]
        [StringLength(100)]
        public string StrUom { get; set; } = null!;
        [Column("numQuantity", TypeName = "numeric(18, 6)")]
        public decimal NumQuantity { get; set; }
        [Column("numItemPrice", TypeName = "numeric(18, 6)")]
        public decimal NumItemPrice { get; set; }
        [Column("numDeliveryValue", TypeName = "numeric(18, 6)")]
        public decimal NumDeliveryValue { get; set; }
        [Column("numTotalDiscountValue", TypeName = "numeric(18, 6)")]
        public decimal NumTotalDiscountValue { get; set; }
        [Column("numTotalShipingValue", TypeName = "numeric(18, 6)")]
        public decimal NumTotalShipingValue { get; set; }
        [Column("numTotalTax", TypeName = "numeric(18, 6)")]
        public decimal NumTotalTax { get; set; }
        [Column("numNetValue", TypeName = "numeric(18, 6)")]
        public decimal NumNetValue { get; set; }
        [Column("intLocationId")]
        public long IntLocationId { get; set; }
        [Column("strLocationName")]
        [StringLength(100)]
        public string StrLocationName { get; set; } = null!;
        [Column("strSpecification")]
        [StringLength(200)]
        public string? StrSpecification { get; set; }
        [Column("isFreeItem")]
        public bool IsFreeItem { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
        [Column("strShipToPartnerContactNo")]
        [StringLength(50)]
        public string? StrShipToPartnerContactNo { get; set; }
        [Column("numMRP", TypeName = "numeric(18, 6)")]
        public decimal NumMrp { get; set; }
        [Column("numCogs", TypeName = "numeric(18, 6)")]
        public decimal NumCogs { get; set; }
        [Column("intReferenceId")]
        public long? IntReferenceId { get; set; }
        [Column("intDLVRowId")]
        public long? IntDlvrowId { get; set; }
        [Column("intParentDLVHId")]
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

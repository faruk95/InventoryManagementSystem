using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblPurchase")]
    public partial class TblPurchase
    {
        [Key]
        [Column("intPurchaseId")]
        public int IntPurchaseId { get; set; }
        [Column("intSupplierId")]
        public int? IntSupplierId { get; set; }
        [Column("dtePurchaseDate", TypeName = "datetime")]
        public DateTime? DtePurchaseDate { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

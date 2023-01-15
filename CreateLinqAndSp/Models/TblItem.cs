using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblItem")]
    public partial class TblItem
    {
        [Key]
        [Column("intItemId")]
        public int IntItemId { get; set; }
        [Column("strItemName")]
        [StringLength(50)]
        public string? StrItemName { get; set; }
        [Column("numStockQuantity", TypeName = "numeric(18, 2)")]
        public decimal? NumStockQuantity { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

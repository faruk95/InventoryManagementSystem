using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblSales")]
    public partial class TblSale
    {
        [Key]
        [Column("intSalesId")]
        public int IntSalesId { get; set; }
        [Column("intCustomerId")]
        public int? IntCustomerId { get; set; }
        [Column("dteSalesDate", TypeName = "datetime")]
        public DateTime? DteSalesDate { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblPartnerType")]
    public partial class TblPartnerType
    {
        [Key]
        [Column("intPartnerTypeId")]
        public int IntPartnerTypeId { get; set; }
        [Column("strPartnerTypeName")]
        [StringLength(50)]
        public string? StrPartnerTypeName { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

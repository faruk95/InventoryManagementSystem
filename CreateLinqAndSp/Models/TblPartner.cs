using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblPartner")]
    public partial class TblPartner
    {
        [Key]
        [Column("intPartnerId")]
        public int IntPartnerId { get; set; }
        [Column("strPartnerName")]
        [StringLength(50)]
        public string? StrPartnerName { get; set; }
        [Column("intPartnerTypeId")]
        public int? IntPartnerTypeId { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

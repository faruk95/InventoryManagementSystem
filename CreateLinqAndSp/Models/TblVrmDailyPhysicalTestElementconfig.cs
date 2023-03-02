using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblVrmDailyPhysicalTestElementconfig")]
    public partial class TblVrmDailyPhysicalTestElementconfig
    {
        [Key]
        [Column("intTestElementId")]
        public long IntTestElementId { get; set; }
        [Column("strTestElementName")]
        [StringLength(150)]
        public string StrTestElementName { get; set; } = null!;
        [Column("intUoMId")]
        public long IntUoMid { get; set; }
        [Column("strUoMName")]
        [StringLength(150)]
        public string StrUoMname { get; set; } = null!;
        [Column("intBusinessUnitId")]
        public long IntBusinessUnitId { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
    }
}

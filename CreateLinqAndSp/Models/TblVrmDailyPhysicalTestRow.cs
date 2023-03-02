using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblVrmDailyPhysicalTestRow")]
    public partial class TblVrmDailyPhysicalTestRow
    {
        [Key]
        [Column("intRowId")]
        public long IntRowId { get; set; }
        [Column("intTestElementId")]
        public long IntTestElementId { get; set; }
        [Column("intDailyPhysicalTestId")]
        public long IntDailyPhysicalTestId { get; set; }
        [Column("numTestElementValue", TypeName = "numeric(18, 6)")]
        public decimal? NumTestElementValue { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
        [Column("intCreatedBy")]
        public long IntCreatedBy { get; set; }
        [Column("dteCreatedAt", TypeName = "datetime")]
        public DateTime DteCreatedAt { get; set; }
    }
}

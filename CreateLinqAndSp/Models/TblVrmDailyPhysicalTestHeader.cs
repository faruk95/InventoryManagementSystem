using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateLinqAndSp.Models
{
    [Table("tblVrmDailyPhysicalTestHeader")]
    public partial class TblVrmDailyPhysicalTestHeader
    {
        [Key]
        [Column("intDailyPhysicalTestId")]
        public long IntDailyPhysicalTestId { get; set; }
        [Column("intBusinessUnitId")]
        public long IntBusinessUnitId { get; set; }
        [Column("intShiftId")]
        public long IntShiftId { get; set; }
        [Column("strShiftName")]
        [StringLength(150)]
        public string StrShiftName { get; set; } = null!;
        [Column("intVRMId")]
        public long IntVrmid { get; set; }
        [Column("strVRMName")]
        [StringLength(150)]
        public string StrVrmname { get; set; } = null!;
        [Column("intItemTypeId")]
        public long IntItemTypeId { get; set; }
        [Column("strItemTypeName")]
        [StringLength(150)]
        public string StrItemTypeName { get; set; } = null!;
        [Column("tmTime")]
        public TimeSpan? TmTime { get; set; }
        [Column("dteDate", TypeName = "date")]
        public DateTime DteDate { get; set; }
        [Column("numInitialTime", TypeName = "numeric(18, 6)")]
        public decimal NumInitialTime { get; set; }
        [Column("numFinalTime", TypeName = "numeric(18, 6)")]
        public decimal NumFinalTime { get; set; }
        [Column("strRemark")]
        [StringLength(500)]
        public string? StrRemark { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
        [Column("intCreatedBy")]
        public long IntCreatedBy { get; set; }
        [Column("dteCreatedAt", TypeName = "datetime")]
        public DateTime DteCreatedAt { get; set; }
        [Column("intUpdatedBy")]
        public long IntUpdatedBy { get; set; }
        [Column("dteUpdateAt", TypeName = "datetime")]
        public DateTime DteUpdateAt { get; set; }
    }
}

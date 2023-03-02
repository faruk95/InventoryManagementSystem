using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CreateLinqAndSp.DTO
{
    public class DailyPhysicalTest
    {

    }
    public class PhysicalTestDTO
    {

        public long IntBusinessUnitId { get; set; }
        public long IntShiftId { get; set; }
        public string StrShiftName { get; set; } = null!;
        public long IntVrmid { get; set; }
        public string StrVrmname { get; set; } = null!;
        public long IntItemTypeId { get; set; }
        public string StrItemTypeName { get; set; } = null!;
       // public TimeSpan TmTime { get; set; }
        public DateTime DteDate { get; set; }
        public decimal NumInitialTime { get; set; }
        public decimal NumFinalTime { get; set; }
        public string? StrRemark { get; set; }
        public bool IsActive { get; set; }
        public long IntCreatedBy { get; set; }
        public DateTime DteCreatedAt { get; set; }
        public long IntUpdatedBy { get; set; }
      
        public DateTime DteUpdateAt { get; set; }
        public List<PhysicalTestRow> row { get; set; }

        public long IntTestElementId { get; set; }

        public string StrTestElementName { get; set; } = null!;
  
        public long IntUoMid { get; set; }
    
        public string StrUoMname { get; set; } = null!;
    }
    public class PhysicalTestRow
    {
       
        public long IntTestElementId { get; set; }
        public long IntDailyPhysicalTestId { get; set; }
        public decimal? NumTestElementValue { get; set; }
        public bool IsActive { get; set; }
        public long IntCreatedBy { get; set; }
        public DateTime DteCreatedAt { get; set; }

    }
}

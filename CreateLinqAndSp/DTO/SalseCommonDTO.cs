using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CreateLinqAndSp.DTO
{
    public class SalseCommonDTO
    {
        public SalseTabeHead Head { get; set; }
        public List<salseTableDetailRow> row {get;set;}
    }

    public class salseTableDetailRow
    {


        [Column("intItemId")]
        public int? ItemId { get; set; }
        [Column("numItemQuantity", TypeName = "numeric(18, 2)")]
        public decimal? ItemQuantity { get; set; }
        [Column("numUnitPrice", TypeName = "numeric(18, 2)")]
        public decimal? UnitPrice { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }

    public class SalseTabeHead
    {

        [Column("intCustomerId")]
        public int? IntCustomerId { get; set; }
        [Column("dteSalesDate", TypeName = "datetime")]
        public DateTime? DteSalesDate { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

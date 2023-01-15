using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CreateLinqAndSp.DTO
{
    public class CrateItemList
    {
        public int ItemId { get; set; }

        [Column("strItemName")]
        [StringLength(50)]
        public string? StrItemName { get; set; }
        [Column("numStockQuantity", TypeName = "numeric(18, 2)")]
        public decimal? NumStockQuantity { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

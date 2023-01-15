using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateLinqAndSp.DTO
{
    public class PurchesDetailDTO
    {
     
        [Column("itemId")]
        public int? ItemId { get; set; }
        [Column("itemQuantity", TypeName = "numeric(18, 2)")]
        public decimal? ItemQuantity { get; set; }
        [Column("initPrice", TypeName = "numeric(18, 2)")]
        public decimal? UnitPrice { get; set; }
      
    }
}

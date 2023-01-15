using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CreateLinqAndSp.DTO
{
    public class PurchesHeaderDTO
    {

        public int? IntSupplierId { get; set; }      
        public DateTime? DtePurchaseDate { get; set; }
   
        public bool? IsActive { get; set; }
    }
}

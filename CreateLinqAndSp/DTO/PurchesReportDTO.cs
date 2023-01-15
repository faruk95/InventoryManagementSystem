using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateLinqAndSp.DTO
{
    public class PurchesReportDTO
    {
       
        public string? StrItemName { get; set; }


        public decimal? TotalQuantity { get; set; }
       
        public decimal? TotalPrice { get; set; }

       
        
    }
    
}

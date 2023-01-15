using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CreateLinqAndSp.DTO
{
    public class CreatePartnerSingleDTO
    {
 
      
        [Column("strPartnerName")]
        [StringLength(50)]
        public string? StrPartnerName { get; set; }
        [Column("intPartnerTypeId")]
        public int? IntPartnerTypeId { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

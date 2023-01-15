using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CreateLinqAndSp.DTO
{
    public class CreateDTOPartnerType
    {
       
       
        [Column("strPartnerTypeName")]
        [StringLength(50)]
        public string? StrPartnerTypeName { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}

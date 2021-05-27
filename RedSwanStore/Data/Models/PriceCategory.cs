using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSwanStore.Data.Models
{
    public class PriceCategory
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string UrlId { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal MinPrice { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal MaxPrice { get; set; }
    }
}
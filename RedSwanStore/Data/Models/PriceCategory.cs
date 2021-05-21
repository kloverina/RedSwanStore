using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class PriceCategory
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        
        [Required]
        public decimal MinPrice { get; set; }
        
        [Required]
        public decimal MaxPrice { get; set; }
    }
}
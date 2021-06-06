using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(5)]
        public string UserEmail { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string GameUrl { get; set; }
    }
}
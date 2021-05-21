using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class Feature
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
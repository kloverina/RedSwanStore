using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class Feature
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string UrlId { get; set; }
        
        // 'many to many' relation with GameFilter
        public List<GameFilter> GameFilters { get; set; } = new List<GameFilter>();
    }
}
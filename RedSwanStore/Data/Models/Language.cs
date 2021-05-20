using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class Language
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        public bool Voice { get; set; }
        
        [Required]
        public bool Text { get; set; }
        
        // for 'many to many' relation with GameSystemRequirements
        public List<GameSystemRequirements> GameSystemRequirementses { get; set; } = new List<GameSystemRequirements>();
    }
}
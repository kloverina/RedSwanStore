﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class Os
    {
        public int Id { get; set;}
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        
        [StringLength(50)]
        public string ExtraInfo { get; set; }
        
        // 'many to many' relation with GameSystemRequirements
        public List<GameSystemRequirement> GameSystemRequirements { get; set; } = new List<GameSystemRequirement>();
    }
}
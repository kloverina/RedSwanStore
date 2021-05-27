using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSwanStore.Data.Models
{
    public class Game
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string GameUrl { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Developer { get; set; }
        
        public GameInfo GameInfo { get; set; } // 'single to single' relation with GameInfo
        
        public GameSystemRequirement GameSystemRequirements { get; set; } // for 'single to single' relation with GameSystemRequirements
        
        public GameMedia GameMedia { get; set; } // 'single to single' relation with GameMedia
        
        // 'single to many' relation with UserLibraryGame
        public List<UserLibraryGame> UserLibraryGames { get; set; } = new List<UserLibraryGame>(); 
        
        // 'single to single' relation with GameFilter
        public GameFilter GameFilter { get; set; }
    }
}
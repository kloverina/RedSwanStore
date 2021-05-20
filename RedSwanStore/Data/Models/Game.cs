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
        [StringLength(100, MinimumLength = 2)]
        public string Developer { get; set; }
        
        // for 'single to single' relation with GameInfo
        public GameInfo GameInfo { get; set; }
        
        // for 'single to single' relation with GameSystemRequirements
        public GameSystemRequirements GameSystemRequirements { get; set; }
    }
}
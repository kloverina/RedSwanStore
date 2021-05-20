using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class GameMedia
    {
        public int Id { get; set; }
        
        [Required]
        public Game Game { get; set; }
        public int GameId { get; set; } // 'single to single' relation with Game
        
        public string Trailers { get; set; }
        
        public string Screenshots { get; set; }
    }
}
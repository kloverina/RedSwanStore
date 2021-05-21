using System;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class GameInfo
    {
        public int Id { get; set; }
        
        public Game Game { get; set; }
        public int GameId { get; set; } // 'single to single' relation with Game
        
        [Required]
        public string Cover { get; set; }
        
        [Required] 
        public decimal Price { get; set; }
        
        [Required] 
        public float Discount { get; set; }
        
        [Required]
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        public byte Rating { get; set; }
        
        [StringLength(500)]
        public string ShortDescription { get; set; }
        
        [StringLength(10000)]
        public string DetailedDescription { get; set; }
        
        [StringLength(500)]
        public string LegalInfo { get; set; }
    }
}
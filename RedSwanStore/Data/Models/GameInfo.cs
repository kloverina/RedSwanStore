﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSwanStore.Data.Models
{
    public class GameInfo
    {
        public int Id { get; set; }
        
        public Game? Game { get; set; }
        public int GameId { get; set; } // 'single to single' relation with Game
        
        [Required]
        public string Cover { get; set; }
        
        [Required] 
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        
        [Required] 
        public float Discount { get; set; }
        
        public DateTime DiscountEndDate { get; set; }
        
        [Required]
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        public byte Rating { get; set; }
        
        [StringLength(1000)]
        public string ShortDescription { get; set; }
        
        [StringLength(10000)]
        public string DetailedDescription { get; set; }
        
        [StringLength(1000)]
        public string LegalInfo { get; set; }
    }
}
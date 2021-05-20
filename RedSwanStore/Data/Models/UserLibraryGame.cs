using System;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class UserLibraryGame
    {
        public int Id { get; set; }
        
        public UserLibrary UserLibrary { get; set; }
        public int UserLibraryId { get; set; } // 'single to many' relation with UserLibrary
        
        public Game Game { get; set; }
        public int GameId { get; set; } // 'single to many' relation with Game
        
        [Required]
        public uint HoursPlayed { get; set; }
        
        public DateTime LastPlayed { get; set; }
        
        [Required]
        public bool IsFavourite { get; set; }
    }
}
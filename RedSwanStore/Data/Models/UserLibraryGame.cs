using System;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class UserLibraryGame
    {
        public int Id { get; set; }
        
        public UserLibrary UserLibrary { get; set; }
        public int UserLibraryId { get; set; } // for 'single to many' relation with UserLibrary

        [Required]
        public Game Game { get; set; }
        public int GameId { get; set; }
        
        [Required]
        public uint HoursPlayed { get; set; }
        
        public DateTime LastPlayed { get; set; }
        
        [Required]
        public bool IsFavourite { get; set; }
    }
}
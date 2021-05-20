using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class UserLibrary
    {
        public int Id { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; } // 'single to single' relation with User
        
        // 'single to many' relation with UserLibraryGame
        public List<UserLibraryGame> UserLibraryGames { get; set; } = new List<UserLibraryGame>();
    }
}
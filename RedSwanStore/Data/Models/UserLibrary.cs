using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class UserLibrary
    {
        public int Id { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; } // for 'single to single' relation with User
        
        public List<UserLibraryGame> UserLibraryGames { get; set; } = new List<UserLibraryGame>();
    }
}
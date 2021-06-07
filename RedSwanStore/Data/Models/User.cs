using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSwanStore.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string UserUrl { get; set; }
        
        [Required]
        public bool IsAdmin { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Login { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Email { get; set; }
        
        [Required]
        public bool GetNewsOnEmail { get; set; }
        
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        
        public string Photo { get; set; }

        [Column(TypeName = "money")]
        public decimal Balance { get; set; }
        
        public UserLibrary? Library { get; set; } // 'single to single' relation with Library
    }
}
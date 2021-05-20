using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
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
        [MinLength(8)]
        public string Password { get; set; }
        
        [Required]
        public string Photo { get; set; }
    }
}
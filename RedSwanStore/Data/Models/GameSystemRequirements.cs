using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedSwanStore.Data.Models
{
    public class GameSystemRequirements
    {
        public int Id { get; set; }
        
        [Required]
        public Game Game { get; set; }
        public int GameId { get; set; }
        
        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string MinCpu { get; set; }
        
        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string MaxCpu { get; set; }
        
        [Required]
        public uint MinRamMB { get; set; }
        
        [Required]
        public uint MaxRamMB { get; set; }
        
        [Required]
        public uint DiskSpaceMB { get; set; }
        
        [Required]
        public byte DirectX { get; set; }
        
        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string MinGpu { get; set; }
        
        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string MaxGpu { get; set; }
        
        [Required]
        public List<Os> SupportedOses { get; set; } = new List<Os>();
        
        [StringLength(200)]
        public string ExtraInfo { get; set; }
        
        [Required]
        public List<Language> SupportedLanguages { get; set; } = new List<Language>();
    }
}
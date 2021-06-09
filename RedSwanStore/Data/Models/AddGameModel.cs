using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSwanStore.Data.Models
{
    [NotMapped]
    public class AddGameModel
    {
        public string Name { get; set; }
        public string GameUrl { get; set; }
        public string Developer { get; set; }
        public string Cover { get; set; }
        public decimal Price { get; set; }
        public string Discount { get; set; }
        public string DiscountEndDate { get; set; }
        public string ReleaseDate { get; set; }
        public byte Rating { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string LegalInfo { get; set; }
        public string MinCpu { get; set; }
        public string MaxCpu { get; set; }
        public uint MinRamMB { get; set; }
        public uint MaxRamMB { get; set; }
        public uint DiskSpaceMB { get; set; }
        public byte DirectX { get; set; }
        public string MinGpu { get; set; }
        public string MaxGpu { get; set; }
        public string SupportedOses { get; set; }
        public string ExtraInfo { get; set; }
        public string SupportedVoiceLanguages { get; set; }
        public string SupportedTextLanguages { get; set; }
        public string Trailers { get; set; }
        public string Screenshots { get; set; }
        public string[] Features { get; set; }
        public string[] Genres { get; set; }
    }
}
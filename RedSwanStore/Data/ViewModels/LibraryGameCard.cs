using System;

namespace RedSwanStore.Data.ViewModels
{
    public class LibraryGameCard
    {
        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public uint HoursPlayed { get; set; }
        public DateTime LastPlayed { get; set; }
    }
}
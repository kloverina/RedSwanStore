using System;

namespace RedSwanStore.Data.ViewModels
{
    public class LibraryGameCard
    {
        public int GameId { get; set; }
        public bool IsFavourite { get; set; }
        public string CoverUrl { get; set; }
        public string Title { get; set; }
        public uint HoursPlayed { get; set; }
        public DateTime LastPlayed { get; set; }
    }
}
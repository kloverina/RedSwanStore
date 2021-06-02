using System.Collections.Generic;
using RedSwanStore.Controllers;
using RedSwanStore.Data.Interfaces;

namespace RedSwanStore.Data.ViewModels
{
    public class LibraryViewModel
    {
        public static Dictionary<string, KeyValuePair<string, LibrarySorts>> SortingItems { get; } =
            new Dictionary<string, KeyValuePair<string, LibrarySorts>> {
                {"recently-played", new KeyValuePair<string, LibrarySorts>("Недавние", LibrarySorts.RecentlyPlayed)},
                {"alphabetically", new KeyValuePair<string, LibrarySorts>("По алфавиту", LibrarySorts.Alphabetically)},
            };
        
        public static Dictionary<string, KeyValuePair<string, LibraryFilters>> FilterOptions { get; } =
            new Dictionary<string, KeyValuePair<string, LibraryFilters>> {
                {"all", new KeyValuePair<string, LibraryFilters>("Все", LibraryFilters.All)},
                {"favourite", new KeyValuePair<string, LibraryFilters>("Избранные", LibraryFilters.Favourite)}
            };

        public IEnumerable<LibraryGameCard> GameCards { get; set; }
    }
}
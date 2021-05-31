using System.Collections.Generic;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.ViewModels
{
    public class HomeViewModel
    {
        public static Dictionary<string, KeyValuePair<string, SortingTypes>> SortingItems { get; } =
            new Dictionary<string, KeyValuePair<string, SortingTypes>> {
                {"default", new KeyValuePair<string, SortingTypes>("Актуальность", SortingTypes.Default)},
                {"release-date", new KeyValuePair<string, SortingTypes>("Дата выхода", SortingTypes.ReleaseDate)},
                {"alphabetically", new KeyValuePair<string, SortingTypes>("По алфавиту", SortingTypes.Alphabetically)},
                {"price-ascending", new KeyValuePair<string, SortingTypes>("Цена: По возрастанию", SortingTypes.PriceAscending)},
                {"price-descending", new KeyValuePair<string, SortingTypes>("Цена: По убыванию", SortingTypes.PriceDescending)}
        };
        
        
        public IEnumerable<GameCard> Games { get; set; }
        public IEnumerable<Genre> GenresFilters { get; set; }
        public IEnumerable<Feature> FeaturesFilters { get; set; }
        public IEnumerable<PriceCategory> PriceFilters { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Data.ViewModels;
using RedSwanStore.Utils;

namespace RedSwanStore.Controllers
{
    [Route("")]
    [Route("store")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly IGameRepo gamesTable;
        private readonly IFeatureRepo featuresTable;
        private readonly IGenreRepo genresTable;
        private readonly IPriceCategoryRepo priceCategoriesTable;

        private HomeViewModel homeViewModel;

        /// <summary>
        /// Create a collection of game cards from specified collection of games.
        /// </summary>
        /// <param name="games">The collection of games to create cards from.</param>
        /// <returns>Collection of game cards.</returns>
        private IEnumerable<GameCard> CreateGameCards(IEnumerable<Game> games)
        {
            IEnumerable<GameCard> gameCards = games.Select(g => new GameCard {
                Title = g.Name,
                Developer = g.Developer,
                CoverUrl = g.GameInfo.Cover,
                Discount = g.GameInfo.Discount.ConvertToPercents(),
                Price = (g.GameInfo.Price * (decimal) (1 - g.GameInfo.Discount)).ConvertToPrice()
            });

            return gameCards;
        }


        public HomeController(IGameRepo gameR, IFeatureRepo featureR, IGenreRepo genreR, IPriceCategoryRepo categoryR)
        {
            gamesTable = gameR;
            featuresTable = featureR;
            genresTable = genreR;
            priceCategoriesTable = categoryR;
                
            homeViewModel = new HomeViewModel {
                FeaturesFilters = featuresTable.GetAllFeatures(),
                GenresFilters = genresTable.GetAllGenres(),
                PriceFilters = priceCategoriesTable.GetAllCategories()
            };
        }
        
        
        [Route("")]
        public IActionResult Index()
        {
            homeViewModel.Games = CreateGameCards(gamesTable.GetAllGames());
            
            return View(homeViewModel);
        }


        
        [Route("sort-and-filter")]
        [HttpPost]
        public IActionResult SortAndFilter(string[] genresIds, string[] featuresIds, string[] pricesIds, string[] sortsIds )
        {
            // construct all filters by data received from page
            GameFilter? filter = new GameFilter {
                Genres = genresIds.Select(id => genresTable.GetGenreByUrlId(id)).ToList()!,
                Features = featuresIds.Select(id => featuresTable.GetFeatureByUrlId(id)).ToList()!
            };

            if (filter.Genres.Count == 0 && filter.Features.Count == 0)
                filter = null;

            // construct all categories by data received from page
            List<PriceCategory> categories = pricesIds.Select(
                id => priceCategoriesTable.GetCategoryByUrlId(id)
            ).ToList()!;

            // get sort type by data received from page
            SortingTypes sortType = HomeViewModel.SortingItems[sortsIds[0]].Value;


            // get filtered and sorted games
            IEnumerable<Game> filteredAndSortedGames = gamesTable
                .GetGamesByFilter(filter!, categories)
                .SortBy(sortType);

            IEnumerable<GameCard> gameCards = CreateGameCards(filteredAndSortedGames);

            return PartialView("_GamesListPartial", gameCards);
        }
    }
}
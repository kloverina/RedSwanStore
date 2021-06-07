using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Data.ViewModels;
using RedSwanStore.Utils;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace RedSwanStore.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("")]
    [Microsoft.AspNetCore.Mvc.Route("store")]
    [Microsoft.AspNetCore.Mvc.Route("home")]
    public class HomeController : Controller
    {
        private readonly IGameRepo gamesTable;
        private readonly IFeatureRepo featuresTable;
        private readonly IGenreRepo genresTable;
        private readonly IPriceCategoryRepo priceCategoriesTable;
        private readonly IUserRepo usersTable;

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
                Price = g.GameInfo.Price == 0 ? "Бесплатно" : (g.GameInfo.Price * (decimal) (1 - g.GameInfo.Discount)).ConvertToPrice(),
                Id = g.GameUrl
            });

            return gameCards;
        }


        public HomeController(IGameRepo gameR, IFeatureRepo featureR, IGenreRepo genreR, IPriceCategoryRepo categoryR, IUserRepo userR)
        {
            gamesTable = gameR;
            featuresTable = featureR;
            genresTable = genreR;
            priceCategoriesTable = categoryR;
            usersTable = userR;
                
            homeViewModel = new HomeViewModel {
                FeaturesFilters = featuresTable.GetAllFeatures(),
                GenresFilters = genresTable.GetAllGenres(),
                PriceFilters = priceCategoriesTable.GetAllCategories()
            };
        }
        
        
        [Microsoft.AspNetCore.Mvc.Route("")]
        public IActionResult Index()
        {
            homeViewModel.Games = CreateGameCards(gamesTable.GetAllGames());
            
            if (User.Identity.IsAuthenticated)
            {
                User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
                
                ViewData["userLogin"] = user.Login;
                ViewData["userUrl"] = user.UserUrl;
                ViewData["userPhoto"] = user.Photo;
                ViewData["userBalance"] = user.Balance.ConvertToPrice();
                ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            }
            
            return View(homeViewModel);
        }


        [Microsoft.AspNetCore.Mvc.Route("sort-and-filter")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
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
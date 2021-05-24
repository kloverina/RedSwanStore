using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

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
        private readonly IPriceCategoryRepo categoryTable;

        public HomeController(IGameRepo gamesTable, IFeatureRepo featuresTable, IGenreRepo genresTable, IPriceCategoryRepo categoryTable)
        {
            this.gamesTable = gamesTable;
            this.featuresTable = featuresTable;
            this.genresTable = genresTable;
            this.categoryTable = categoryTable;
        }
        
        [Route("")]
        public ViewResult Index()
        {
            var filter = new GameFilter {
 
            };

            var priceCategories = new List<PriceCategory> {
                categoryTable.GetCategoryByName("Цена для галочки"),
                categoryTable.GetCategoryByName("Ниже среднего"),
                categoryTable.GetCategoryByName("Очень многа")
            };
            
            var allGames = gamesTable.GetGamesByFilter(filter, priceCategories);
            var game = new List<Game> {gamesTable.GetGameByUrl("evans-remains") };
            
            return View(allGames);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Data.ViewModels;
using RedSwanStore.Utils;

namespace RedSwanStore.Controllers
{
    public enum LibrarySorts
    {
        RecentlyPlayed,
        Alphabetically
    }

    public enum LibraryFilters
    {
        All,
        Favourite
    }
    
    [Route("library")]
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly IUserRepo usersTable;
        private readonly IGameRepo gamesTable;
        private readonly IGameLibraryRepo libraryGamesTable;

        
        private User currentUser;
        private LibraryViewModel libraryViewModel;
        
        public LibraryController(IUserRepo userR, IGameRepo gameR, IGameLibraryRepo libraryGamesR)
        {
            usersTable = userR;
            gamesTable = gameR;
            libraryGamesTable = libraryGamesR;
            
            libraryViewModel = new LibraryViewModel();
        }
        
        [Route("user")]
        [HttpGet]
        public IActionResult Library()
        {
            currentUser = GetAuthenticatedUser();

            var library = currentUser.Library;
            
            if (library != null)
            {
                var libraryGames = library.UserLibraryGames;

                if (!libraryGames.Any())
                    libraryViewModel.GameCards = new List<LibraryGameCard>();
                else
                    libraryViewModel.GameCards = CreateLibraryGameCards(libraryGames);
            }
            else
                libraryViewModel.GameCards = new List<LibraryGameCard>();

            ViewBag.User = currentUser;
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            

            return View(libraryViewModel);
        }

        [Route("set-favourite")]
        [HttpPost]
        public void SetFavourite(int gameId, bool isFavourite)
        {
            UserLibraryGame game = libraryGamesTable.GetGameById(gameId)!;
            libraryGamesTable.SetFavourite(game, isFavourite);
        }
        
        [HttpPost]
        public IActionResult SortAndFilter(string sortId, string filterId)
        {
            currentUser = GetAuthenticatedUser();
            var library = currentUser.Library;
            
            if (library != null)
            {
                var libraryGames = library.UserLibraryGames;

                if (!libraryGames.Any())
                    libraryViewModel.GameCards = new List<LibraryGameCard>();
                else
                    libraryViewModel.GameCards = CreateLibraryGameCards(
                        libraryGames,
                        LibraryViewModel.SortingItems[sortId].Value,
                        LibraryViewModel.FilterOptions[filterId].Value
                    );
            }
            else
                libraryViewModel.GameCards = new List<LibraryGameCard>();

            return PartialView("_LibraryGamesListPartial", libraryViewModel.GameCards);
        }


        private IEnumerable<LibraryGameCard> CreateLibraryGameCards(
            IEnumerable<UserLibraryGame> libraryGames, 
            LibrarySorts sort = LibrarySorts.Alphabetically, 
            LibraryFilters filter = LibraryFilters.All
        )
        {
            IEnumerable<UserLibraryGame> userLibraryGames = libraryGames.ToList();
            
            IEnumerable<UserLibraryGame> filteredGames =
                (filter == LibraryFilters.Favourite ? userLibraryGames.Where(lg => lg.IsFavourite) : userLibraryGames);

            IEnumerable<LibraryGameCard> libraryGameCards = filteredGames.Select(lg => new LibraryGameCard{
                GameId = lg.Id,
                HoursPlayed = lg.HoursPlayed,
                LastPlayed = lg.LastPlayed.Date,
                IsFavourite = lg.IsFavourite,
                CoverUrl = gamesTable.GetGameById(lg.GameId)!.GameInfo.Cover,
                Title = gamesTable.GetGameById(lg.GameId)!.Name
            });

            if (sort == LibrarySorts.Alphabetically)
                libraryGameCards = libraryGameCards.OrderBy(lgc => lgc.Title);
            else
                libraryGameCards = libraryGameCards.OrderByDescending(lgc => lgc.LastPlayed);

            return libraryGameCards;
        }

        private User GetAuthenticatedUser()
        {
            var email = User.Identity.Name;
            return usersTable.GetUserByEmail(email!)!;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Data.ViewModels;

namespace RedSwanStore.Controllers
{
    [Route("games-management")]
    [Authorize]
    public class GameManagement : Controller
    {
        private readonly IUserRepo usersTable;
        private readonly IGameRepo gamesTable;
        private readonly IFeatureRepo featuresTable;
        private readonly IGenreRepo genresTable;

        public GameManagement(IUserRepo userR, IGameRepo gameR, IFeatureRepo featureR, IGenreRepo genreR)
        {
            usersTable = userR;
            gamesTable = gameR;
            featuresTable = featureR;
            genresTable = genreR;
        }

        private GameManagementViewModel viewModel;
        
        [Route("add-game")]
        [HttpGet]
        public IActionResult AddGame()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            
            ViewBag.User = user;
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";

            if (!user.IsAdmin)
                return RedirectToAction("AccessDenied", "ErrorPage");
            
            viewModel = new GameManagementViewModel{
                Genres = genresTable.GetAllGenres(),
                Features = featuresTable.GetAllFeatures()
            };
            
            return View(viewModel);
        }

        
        [Route("edit-game")]
        [HttpGet]
        public IActionResult EditGame(int gameId)
        {
            Game? game = gamesTable.GetGameById(gameId);

            if (game is null || game.IsRemoved)
                return RedirectToAction("GameNotFound", "ErrorPage");
            
            
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            ViewBag.User = user;
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            
            if (!user.IsAdmin)
                return RedirectToAction("AccessDenied", "ErrorPage");
            
            usersTable.SetCurrentlyEditedGame(user, game.Id);
            
            viewModel = new GameManagementViewModel {
                Game = game,
                Genres = genresTable.GetAllGenres(),
                Features = featuresTable.GetAllFeatures()
            };

            return View(viewModel);
        }

        
        [Route("delete-game")]
        [HttpGet]
        public IActionResult DeleteGame(int gameId)
        {
            Game? game = gamesTable.GetGameById(gameId);

            if (game is null || game.IsRemoved)
                return RedirectToAction("GameNotFound", "ErrorPage");
            
            
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            ViewBag.User = user;
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            
            if (!user.IsAdmin)
                return RedirectToAction("AccessDenied", "ErrorPage");
            
            
            usersTable.SetCurrentlyEditedGame(user, game.Id);
            ViewBag.GameTitle = game.Name;

            return View();
        }


        [Route("remove-from-store")]
        [HttpPost]
        public IActionResult HideFromStore()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            Game game = gamesTable.GetGameById(user.CurrentlyEditedGameId)!;
            
            gamesTable.RemoveFromStore(game);

            return Content("/home");
        }


        [Route("save-added-game")]
        [HttpPost]
        public IActionResult SaveAddedGame(AddGameModel data)
        {
            try
            {
                List<Genre> genres = data.Genres.Select(g => genresTable.GetGenreByUrlId(g)!).ToList();
                List<Feature> features = data.Features.Select(f => featuresTable.GetFeatureByUrlId(f)!).ToList();
            
                var gameFilter = new GameFilter {
                    Features = features,
                    Genres = genres
                };
            
                var gameMedia = new GameMedia {
                    Screenshots = data.Screenshots,
                    Trailers = data.Trailers
                };
            
                var gameSystemRequirements = new GameSystemRequirement {
                    MinCpu = data.MinCpu,
                    MaxCpu = data.MaxCpu,
                    MinRamMB = data.MinRamMB,
                    MaxRamMB = data.MaxRamMB,
                    MinGpu = data.MinGpu,
                    MaxGpu = data.MaxGpu,
                    DiskSpaceMB = data.DiskSpaceMB,
                    DirectX = data.DirectX,
                    SupportedOses = data.SupportedOses,
                    ExtraInfo = data.ExtraInfo == null ? "" : data.ExtraInfo,
                    SupportedTextLanguages = data.SupportedTextLanguages,
                    SupportedVoiceLanguages = data.SupportedVoiceLanguages
                };

                var discount = float.Parse(data.Discount.Replace('.', ','));
                
                DateTime discountEndDate = discount != 0 && !string.IsNullOrEmpty(data.DiscountEndDate)
                    ? Convert.ToDateTime(data.DiscountEndDate)
                    : DateTime.MinValue;

                var releaseDate = Convert.ToDateTime(data.ReleaseDate);
            
                var gameInfo = new GameInfo {
                    Cover = data.Cover,
                    Price = data.Price,
                    Discount = discount,
                    DiscountEndDate = discountEndDate,
                    ReleaseDate = releaseDate,
                    Rating = data.Rating,
                    ShortDescription = data.ShortDescription,
                    DetailedDescription = data.DetailedDescription,
                    LegalInfo = data.LegalInfo
                };
            
                var game = new Game {
                    Name = data.Name,
                    Developer = data.Developer,
                    GameUrl = data.GameUrl,
                    GameInfo = gameInfo,
                    GameSystemRequirements = gameSystemRequirements,
                    GameMedia = gameMedia,
                    GameFilter = gameFilter
                };

                var success = gamesTable.AddGame(game);

                if (!string.IsNullOrEmpty(success))
                    return Content(success);
            }
            catch (Exception e)
            {
                return Content($"{e.Message}\n{e.StackTrace}");
            }

            return Content("");
        }


        [Route("save-edited-game")]
        [HttpPost]
        public IActionResult SaveEditedGame(AddGameModel data)
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            Game game = gamesTable.GetGameById(user.CurrentlyEditedGameId)!;
            
            try
            {
                List<Genre> genres = data.Genres.Select(g => genresTable.GetGenreByUrlId(g)!).ToList();
                List<Feature> features = data.Features.Select(f => featuresTable.GetFeatureByUrlId(f)!).ToList();

                game.GameFilter.Features = features;
                game.GameFilter.Genres = genres;


                game.GameMedia.Screenshots = data.Screenshots is null ? "" : data.Screenshots;
                game.GameMedia.Trailers = data.Trailers is null ? "" : data.Trailers;


                game.GameSystemRequirements.MinCpu = data.MinCpu == null ? "" : data.MinCpu;
                game.GameSystemRequirements.MaxCpu = data.MaxCpu == null ? "" : data.MaxCpu;
                game.GameSystemRequirements.MinRamMB = data.MinRamMB;
                game.GameSystemRequirements.MaxRamMB = data.MaxRamMB;
                game.GameSystemRequirements.MinGpu = data.MinGpu == null ? "" : data.MinGpu;
                game.GameSystemRequirements.MaxGpu = data.MaxGpu == null ? "" : data.MaxGpu;
                game.GameSystemRequirements.DiskSpaceMB = data.DiskSpaceMB;
                game.GameSystemRequirements.DirectX = data.DirectX;
                game.GameSystemRequirements.SupportedOses = data.SupportedOses == null ? "" : data.SupportedOses;
                game.GameSystemRequirements.ExtraInfo = data.ExtraInfo == null ? "" : data.ExtraInfo;
                game.GameSystemRequirements.SupportedTextLanguages = data.SupportedTextLanguages == null ? "" : data.SupportedTextLanguages;
                game.GameSystemRequirements.SupportedVoiceLanguages = data.SupportedVoiceLanguages == null ? "" : data.SupportedVoiceLanguages;
                

                var discount = float.Parse(data.Discount.Replace('.', ','));
                
                DateTime discountEndDate = discount != 0 && !string.IsNullOrEmpty(data.DiscountEndDate)
                    ? Convert.ToDateTime(data.DiscountEndDate)
                    : DateTime.MinValue;

                var releaseDate = Convert.ToDateTime(data.ReleaseDate);


                game.GameInfo.Cover = data.Cover == null ? "" : data.Cover;
                game.GameInfo.Price = data.Price;
                game.GameInfo.Discount = discount;
                game.GameInfo.DiscountEndDate = discountEndDate;
                game.GameInfo.ReleaseDate = releaseDate;
                game.GameInfo.Rating = data.Rating;
                game.GameInfo.ShortDescription = data.ShortDescription == null ? "" : data.ShortDescription;
                game.GameInfo.DetailedDescription = data.DetailedDescription == null ? "" : data.DetailedDescription;
                game.GameInfo.LegalInfo = data.LegalInfo == null ? "" : data.LegalInfo;


                game.Name = data.Name == null ? "" : data.Name;
                game.Developer = data.Developer == null ? "" : data.Developer;
                game.GameUrl = data.GameUrl == null ? "" : data.GameUrl;
                

                var success = gamesTable.UpdateGame(game);

                if (!string.IsNullOrEmpty(success))
                    return Content(success);
            }
            catch (Exception e)
            {
                return Content($"{e.Message}\n{e.StackTrace}");
            }

            return Content($"/game?gameid={game.GameUrl}");
        }


        [Route("cancel")]
        [HttpPost]
        public IActionResult CancelEdit()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            
            var gameUrl = gamesTable.GetGameById(user.CurrentlyEditedGameId)!.GameUrl;
            return Content($"/game?gameid={gameUrl}");
        }
    }
    
}
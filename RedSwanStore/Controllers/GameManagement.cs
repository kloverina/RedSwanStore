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

                var discount = float.Parse(data.Discount);
                
                DateTime discountEndDate = discount != 0 && !string.IsNullOrEmpty(data.DiscountEndDate)
                    ? Convert.ToDateTime(data.Discount)
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
    }
}
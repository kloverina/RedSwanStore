using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Data.ViewModels;
using RedSwanStore.Utils;

namespace RedSwanStore.Controllers
{
    [Route("game")]
    [Authorize]
    public class GameController : Controller
    {
        private readonly IUserRepo usersTable;
        private readonly IGameRepo gamesTable;
        private readonly ICartRepo cartTable;

        public GameController(IUserRepo userR, IGameRepo gameR, ICartRepo cartR)
        {
            usersTable = userR;
            gamesTable = gameR;
            cartTable = cartR;
        }
        
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Game(string gameId)
        {
            bool hasGameInLib = false;
            Game? game = gamesTable.GetGameByUrl(gameId);
            
            if (game == null)
                return RedirectToAction("GameNotFound", "ErrorPage", new {GameId = gameId});
            
            if (User.Identity.IsAuthenticated)
            {
                User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
                ViewBag.User = user;
                ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";

                if (user.Library != null && user.Library.UserLibraryGames.Any())
                    hasGameInLib = user.Library.UserLibraryGames.FirstOrDefault(ulg => ulg.GameId == game.Id) != null;
                
            }

            ViewBag.hasGameInLib = hasGameInLib;
            
            return View(game);
        }


        [Route("purchase-window")]
        [HttpPost]
        public IActionResult GetPurchaseWindow(string gameId)
        {
            Game game = gamesTable.GetGameByUrl(gameId)!;
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            
            // add game to cart to have access to it later
            cartTable.AddItem(user.Email, gameId);

            var price = game.GameInfo.Price;
            var discount = price * (decimal)game.GameInfo.Discount;
            var resultPrice = price - discount;
            var moneyLeft = user.Balance - resultPrice;

            if (moneyLeft < 0)
                return PartialView("_AddFundsPartial");
            
            return PartialView(
                "_BuyUsingWalletPartial",
                new PurchaseViewModel {
                    ActualPrice = price,
                    DiscountAmount = discount,
                    ResultPrice = resultPrice,
                    MoneyLeft = moneyLeft
                }
            );
        }


        [Route("purchase-by-wallet")]
        [HttpPost]
        public void PurchaseByWallet()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            Game game = gamesTable.GetGameByUrl(cartTable.GetItemFor(user.Email)!)!;
            
            var price = game.GameInfo.Price;
            var discount = price * (decimal)game.GameInfo.Discount;
            var resultPrice = price - discount;
            
            if (resultPrice > 0)
                usersTable.UpdateUserBalance(user, -resultPrice);

            var success = usersTable.AddGameToLibrary(user, game);

            if (success)
            {
                EmailService emailService = new EmailService();
                emailService.SendTransactionCommitEmail(new TransactionInfo {
                    UserEmail = user.Email,
                    UserName = user.Name,
                    AccountIdentifier = GenerateAccountIdentifier(user.Email),
                    TransactionIdentifier = GenerateNewTransactionIdentifier(),
                    GameDeveloper = game.Developer,
                    GameTitle = game.Name,
                    ResultPrice = resultPrice
                });
            }
        }

        [Route("purchase-by-card")]
        [HttpPost]
        public void PurchaseByCard()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            Game game = gamesTable.GetGameByUrl(cartTable.GetItemFor(user.Email)!)!;
            
            var price = game.GameInfo.Price;
            var discount = price * (decimal)game.GameInfo.Discount;
            var resultPrice = price - discount;
            
            var success = usersTable.AddGameToLibrary(user, game);

            if (success)
            {
                EmailService emailService = new EmailService();
                emailService.SendTransactionCommitEmail(new TransactionInfo {
                    UserEmail = user.Email,
                    UserName = user.Name,
                    AccountIdentifier = GenerateAccountIdentifier(user.Email),
                    TransactionIdentifier = GenerateNewTransactionIdentifier(),
                    GameDeveloper = game.Developer,
                    GameTitle = game.Name,
                    ResultPrice = resultPrice
                });
            }
        }
        
        
        private string GenerateNewTransactionIdentifier()
        {
            var identifierSeed = RandomNumberGenerator.GetInt32(int.MaxValue);
            var newIdentifier = HashKeccak.createHashOf(
                identifierSeed.ToString(),
                128
            ).Substring(0, 16);
            

            return newIdentifier;
        }

        private string GenerateAccountIdentifier(string userEmail)
        {
            var newIdentifier = HashKeccak.createHashOf(
                userEmail,
                128
            ).Substring(0, 10);
            

            return newIdentifier;
        }
    }
}
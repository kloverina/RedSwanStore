using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Utils;

namespace RedSwanStore.Data.Repositories
{
    public class GameRepo : IGameRepo
    {
        private readonly RedSwanStoreDBContent dbContent;

        public GameRepo(RedSwanStoreDBContent dbContent)
        {
            this.dbContent = dbContent;
        }

        
        /// <summary>
        /// Load all data related to specified game entity from the database.
        /// </summary>
        /// <param name="game">The entity to load the data for.</param>
        private void LoadDataFor(Game game)
        {
            dbContent.Entry(game).Reference(g => g.GameInfo).Load();
            dbContent.Entry(game).Reference(g => g.GameSystemRequirements).Load();
            dbContent.Entry(game).Reference(g => g.GameMedia).Load();
            dbContent.Entry(game).Reference(g => g.GameFilter).Load();
                
            dbContent.Entry(game).Reference(g => g.GameFilter).TargetEntry.Collection(gf => gf.Features).Load();
            dbContent.Entry(game).Reference(g => g.GameFilter).TargetEntry.Collection(gf => gf.Genres).Load();
            
            // dbContent.Entry(game.GameFilter).Collection(gf => gf.Features).Load();
            // dbContent.Entry(game.GameFilter).Collection(gf => gf.Genres).Load();
        }
        
        
        /// <summary>
        /// Get all games from the database.
        /// </summary>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetAllGames()
        {
            IEnumerable<Game> result;

            result = dbContent.Games.ToList();

            foreach (Game game in result)
                LoadDataFor(game);
            
            return result;
        }

        
        /// <summary>
        /// Get the game with specified name from the database.
        /// </summary>
        /// <param name="name">The name of the game to get.</param>
        /// <returns>The game model entity.</returns>
        public Game? GetGameByName(string name)
        {
            Game? result = dbContent.Games.FirstOrDefault(
                g => g.Name == name
            );
            
            if (result != null)
                LoadDataFor(result);

            return result;
        }

        
        /// <summary>
        /// Get the game with specified url from the database.
        /// </summary>
        /// <param name="url">The url fo the game to get.</param>
        /// <returns>The game model entity.</returns>
        public Game? GetGameByUrl(string url)
        {
            Game? result = dbContent.Games.FirstOrDefault(
                g => g.GameUrl == url
            );

            if (result != null)
                LoadDataFor(result);
            
            return result;
        }

        /// <summary>
        /// Get the game with specified id from the database.
        /// </summary>
        /// <param name="id">The id of the game to get.</param>
        /// <returns>The game model entity.</returns>
        public Game? GetGameById(int id)
        {
            Game? result = dbContent.Games.FirstOrDefault(
                g => g.Id == id
            );
            
            if (result != null)
                LoadDataFor(result);

            return result;
        }
        
        /// <summary>
        /// Get all games that support the specified OS from the database.
        /// </summary>
        /// <param name="osName">The OS the games must support.</param>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetGamesByOs(string osName)
        {
            IEnumerable<Game> result = dbContent.Games
                .Include(g => g.GameSystemRequirements)
                .Where(g => g.GameSystemRequirements.SupportedOses.ToLower().Contains(osName.ToLower()))
                .Select(g => g);

            foreach (Game game in result)
                LoadDataFor(game);
                
            return result;
        }

        
        /// <summary>
        /// Get all games that have discount from the database.
        /// </summary>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetGamesByDiscount()
        {
            IEnumerable<Game> result = dbContent.Games
                .Include(g => g.GameInfo)
                .Where(g => g.GameInfo.Discount != 0)
                .Select(g => g);

            foreach (Game game in result)
                LoadDataFor(game);

            return result;
        }

        
        /// <summary>
        /// Get all games that match specified filter and specified price category from the database.
        /// </summary>
        /// <param name="filter">The filter the games must match.</param>
        /// <param name="priceCategory">The price category the games must match.</param>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetGamesByFilter(GameFilter? filter, PriceCategory? priceCategory)
        {
            IEnumerable<Game> result = GetAllGames();

            result = result.Where(g => g.IsMatchFilter(filter) && g.IsMatchPriceCategory(priceCategory));

            return result;
        }
        

        /// <summary>
        /// Get all games that match specified filter and specified price categories from the database.
        /// </summary>
        /// <param name="filter">The filter the games must match.</param>
        /// <param name="priceCategories">The price categories the games must match.</param>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetGamesByFilter(GameFilter? filter, List<PriceCategory> priceCategories)
        {
            IEnumerable<Game> result = new List<Game>();

            if (priceCategories.Any())
            {
                foreach (PriceCategory priceCategory in priceCategories)
                    result = result.Concat(GetGamesByFilter(filter, priceCategory));
            }
            else
                result = GetGamesByFilter(filter, priceCategory: null);

            return result;
        }
        

        /// <summary>
        /// Get all games whose title or developer contains specified substring.
        /// </summary>
        /// <param name="searchString">The substring to get games by.</param>
        /// <returns>The collection of the game models.</returns>
        public IEnumerable<Game> SearchGames(string searchString)
        {
            IEnumerable<Game> result = (
                from Game g in dbContent.Games
                where g.Name.ToLower().Contains(searchString.ToLower())
                    || g.Developer.ToLower().Contains(searchString.ToLower())
                select g
            );

            foreach (Game game in result)
                LoadDataFor(game);

            return result;
        }

        public string? AddGame(Game game)
        {
            try
            {
                dbContent.Games.Add(game);
                dbContent.SaveChanges();
                return null;
            }
            catch (Exception e)
            {
                return $"{e.Message}\n{e.StackTrace}";
            }
        }

        public string? UpdateGame(Game game)
        {
            try
            {
                dbContent.Games.Update(game);
                dbContent.SaveChanges();
                return null;
            }
            catch (Exception e)
            {
                return $"{e.Message}\n{e.StackTrace}";
            }
        }


        public void RemoveFromStore(Game game)
        {
            game.IsRemoved = true;
            dbContent.Games.Update(game);
            dbContent.SaveChanges();
        }
    }
}
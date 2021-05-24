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


        private void LoadDataFor(Game game)
        {
            dbContent.Entry(game).Reference(g => g.GameInfo).Load();
            dbContent.Entry(game).Reference(g => g.GameSystemRequirements).Load();
            dbContent.Entry(game).Reference(g => g.GameMedia).Load();
            dbContent.Entry(game).Reference(g => g.GameFilter).Load();
                
            dbContent.Entry(game.GameFilter).Collection(gf => gf.Features).Load();
            dbContent.Entry(game.GameFilter).Collection(gf => gf.Genres).Load();
        }
        
        
        public IEnumerable<Game> GetAllGames()
        {
            IEnumerable<Game> result = dbContent.Games.ToList();

            foreach (Game game in result)
                LoadDataFor(game);
            
            return result;
        }

        
        public Game GetGameByName(string name)
        {
            Game result = dbContent.Games.FirstOrDefault(
                g => g.Name == name
            );
            
            LoadDataFor(result);

            return result;
        }

        
        public Game GetGameByUrl(string url)
        {
            Game result = dbContent.Games.FirstOrDefault(
                g => g.GameUrl == url
            );

            LoadDataFor(result);
            
            return result;
        }

        
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

        
        public IEnumerable<Game> GetGamesByFilter(GameFilter filter, PriceCategory priceCategory)
        {
            IEnumerable<Game> result = GetAllGames();

            result = result.Where(g => g.IsMatchFilter(filter) && g.IsMatchPriceCategory(priceCategory));

            return result;
        }
        

        public IEnumerable<Game> GetGamesByFilter(GameFilter filter, List<PriceCategory> priceCategories)
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
    }
}
using System.Linq;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Repositories
{
    public class GameLibraryRepo : IGameLibraryRepo
    {
        private readonly RedSwanStoreDBContent dbContent;

        public GameLibraryRepo(RedSwanStoreDBContent dbContent)
        {
            this.dbContent = dbContent;
        }
        
        public void SetFavourite(UserLibraryGame game, bool isFavourite)
        {
            game.IsFavourite = isFavourite;
            dbContent.UserLibraryGames.Update(game);
            dbContent.SaveChanges();
        }

        public UserLibraryGame? GetGameById(int id)
        {
            UserLibraryGame? game = dbContent.UserLibraryGames.FirstOrDefault(g => g.Id == id);
            return game;
        }
    }
}
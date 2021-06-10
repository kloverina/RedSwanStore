using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Interfaces
{
    public interface IGameLibraryRepo
    {
        /// <summary>
        /// Set the user's game as favourite or vice-versa
        /// </summary>
        /// <param name="game">The game to update.</param>
        /// <param name="isFavourite">Set the game as favourite?</param>
        public void SetFavourite(UserLibraryGame game, bool isFavourite);


        /// <summary>
        /// Get the user's game by its id.
        /// </summary>
        /// <param name="id">The id of the game to get.</param>
        public UserLibraryGame? GetGameById(int id);
    }
}
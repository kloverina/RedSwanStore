using System.Collections.Generic;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Interfaces
{
    public enum SortingTypes {
        Default,
        ReleaseDate,
        Alphabetically,
        PriceDescending,
        PriceAscending
    };
    
    /// <summary>
    /// An interface to work with database Game table.
    /// </summary>
    public interface IGameRepo
    {
        /// <summary>
        /// Get all games from the table as collection.
        /// </summary>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetAllGames();

        /// <summary>
        /// Get specified game by its name.
        /// </summary>
        /// <param name="name">The name of the game to get.</param>
        /// <returns>The game model.</returns>
        public Game? GetGameByName(string name);

        /// <summary>
        /// Get specified game by its url.
        /// </summary>
        /// <param name="url">The url of the game to get.</param>
        /// <returns>The game model.</returns>
        public Game? GetGameByUrl(string url);

        /// <summary>
        /// Get the game with specified id from the database.
        /// </summary>
        /// <param name="id">The id of the game to get.</param>
        /// <returns>The game model entity.</returns>
        public Game? GetGameById(int id);

        /// <summary>
        /// Get all games that match specified Os name.
        /// </summary>
        /// <param name="osName">The name of the Os the games must match.</param>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetGamesByOs(string osName);
        
        /// <summary>
        /// Get all games that have discount.
        /// </summary>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetGamesByDiscount();

        /// <summary>
        /// Get all games that match specified filter and price category.
        /// </summary>
        /// <param name="filter">The filter the games must match.</param>
        /// <param name="priceCategory">The price category the games must match.</param>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetGamesByFilter(GameFilter filter, PriceCategory priceCategory);

        /// <summary>
        /// Get all games that match specified filter and price categories.
        /// </summary>
        /// <param name="filter">The filter the games must match.</param>
        /// <param name="priceCategories">The price categories the games must match.</param>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> GetGamesByFilter(GameFilter filter, List<PriceCategory> priceCategories);

        /// <summary>
        /// Get all games whose name matches the specified search string by character.
        /// </summary>
        /// <param name="searchString">The string to search games by.</param>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<Game> SearchGames(string searchString);


        /// <summary>
        /// Add new game to the database. 
        /// </summary>
        /// <param name="game">A game to add.</param>
        /// <returns>True - if game was added successfully.</returns>
        public string? AddGame(Game game);
    }
}
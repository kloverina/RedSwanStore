namespace RedSwanStore.Data.Interfaces
{
    /// <summary>
    /// An interface to work with database Cart Table
    /// </summary>
    public interface ICartRepo
    {
        /// <summary>
        /// Add specified game for specified user to the cart.
        /// </summary>
        /// <param name="userEmail">The email of the user to add item for.</param>
        /// <param name="gameUrl">The url of game to add.</param>
        public void AddItem(string userEmail, string gameUrl);

        /// <summary>
        /// Get the game for the specified user.
        /// </summary>
        /// <param name="userEmail">The email of user to get the game for.</param>
        /// <returns>The url of the game.</returns>
        public string? GetItemFor(string userEmail);
        
        public void DeleteItem(string userEmail);
    }
}
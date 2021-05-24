namespace RedSwanStore.Data.Interfaces
{
    /// <summary>
    /// An interface to work with database GameInfo table.
    /// </summary>
    public interface IGameInfoRepo
    {
        /// <summary>
        /// Check GameInfo table every day at 18.00 and clear discount for the game
        /// which discount end date has come.
        /// </summary>
        public void CheckAndUpdateDiscounts();
    }
}
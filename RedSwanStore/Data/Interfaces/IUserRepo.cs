using System.Collections.Generic;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Interfaces
{
    /// <summary>
    /// An interface to work with database User table.
    /// </summary>
    public interface IUserRepo
    {
        /// <summary>
        /// Get all users from the database as collection.
        /// </summary>
        /// <returns>The collection of game models.</returns>
        public IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Get specified user from the database by their url.
        /// </summary>
        /// <param name="url">The url of the user to get.</param>
        /// <returns>The user model.</returns>
        public User GetUserByUrl(string url);

        /// <summary>
        /// Get specified user from the database by their email.
        /// </summary>
        /// <param name="email">The email of the user to get.</param>
        /// <returns>The user model.</returns>
        public User GetUserByEmail(string email);

        /// <summary>
        /// Change the user's name in the database to the specified one.
        /// </summary>
        /// <param name="user">The user to change the name for.</param>
        /// <param name="name">The new user's name.</param>
        /// <returns>True - if name was successfully updated.</returns>
        public bool UpdateUserName(User user, string name);

        /// <summary>
        /// Change the user's surname in the database to the specified one.
        /// </summary>
        /// <param name="user">The user to change the name for.</param>
        /// <param name="surname">The new user's surname.</param>
        /// <returns>True - if surname was successfully updated.</returns>
        public bool UpdateUserSurname(User user, string surname);

        /// <summary>
        /// Change the user's photo in the database to the specified one.
        /// </summary>
        /// <param name="user">The user to change the photo for.</param>
        /// <param name="photoUrl">The new user's photo url.</param>
        /// <returns>True - if photo url was successfully updated.</returns>
        public bool UpdateUserPhoto(User user, string photoUrl);

        /// <summary>
        /// Change the user's login in the database to the specified one.
        /// </summary>
        /// <param name="user">The user to change the login for.</param>
        /// <param name="login">The new user's login.</param>
        /// <returns>True - if login was successfully updated.</returns>
        public bool UpdateUserLogin(User user, string login);

        /// <summary>
        /// Change user's password in the database to the specified one.
        /// </summary>
        /// <param name="user">The user to change the password for.</param>
        /// <param name="password">The new user's password.</param>
        /// <returns>True - if password was successfully updated.</returns>
        public bool UpdateUserPassword(User user, string password);

        /// <summary>
        /// Change user's balance in the database to the specified one.
        /// </summary>
        /// <param name="user">The user to change the balance for.</param>
        /// <param name="balanceChange">The amount to change user's balance to (can be either positive or negative).</param>
        /// <returns>True - if balance was successfully updated.</returns>
        public bool UpdateUserBalance(User user, decimal balanceChange);

        /// <summary>
        /// Change the value indicating whether the user will receive news on their email or not.
        /// </summary>
        /// <param name="user">The user to change the news getting rule for.</param>
        /// <param name="getNewsOnEmail">Should the user get news on email?</param>
        /// <returns>True - if news getting rule was successfully updated.</returns>
        public bool UpdateUserNewsGetting(User user, bool getNewsOnEmail);

        /// <summary>
        /// Change user's url in the database to the specified one.
        /// </summary>
        /// <param name="user">The user to change the password for.</param>
        /// <param name="url">The new user's url.</param>
        /// <returns>True - if url was successfully updated.</returns>
        public bool UpdateUserUrl(User user, string url);

        /// <summary>
        /// Add a specified game to the user's library in the database.
        /// </summary>
        /// <param name="user">The user to add the game to the library for.</param>
        /// <param name="game">The game to add to the user's library.</param>
        /// <returns>True - if game was successfully added to the user's library.</returns>
        public bool AddGameToLibrary(User user, Game game);

        /// <summary>
        /// Add a new user to the database.
        /// </summary>
        /// <param name="name">The user's name.</param>
        /// <param name="surname">The user's surname.</param>
        /// <param name="login">The user's login.</param>
        /// <param name="password">The user's email.</param>
        /// <param name="email">The user's password.</param>
        /// <param name="getNewsOnEmail">Will the user get news on their email?</param>
        /// <returns>True - if user was successfully added to the database.</returns>
        public bool AddUser(string name, string surname, string login, string email, string password, bool getNewsOnEmail);
    }
}
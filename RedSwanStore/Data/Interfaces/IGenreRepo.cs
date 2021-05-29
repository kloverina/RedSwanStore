using System.Collections;
using System.Collections.Generic;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Interfaces
{
    /// <summary>
    /// An interface to work with database Genre table.
    /// </summary>
    public interface IGenreRepo
    {
        /// <summary>
        /// Get all genres from the table as collection.
        /// </summary>
        /// <returns>The collection of genre models.</returns>
        public IEnumerable<Genre> GetAllGenres();

        /// <summary>
        /// Get specified genre by its name.
        /// </summary>
        /// <param name="name">The name of the genre to get.</param>
        /// <returns>The genre model.</returns>
        public Genre? GetGenreByName(string name);

        /// <summary>
        /// Get specified genre by its url id.
        /// </summary>
        /// <param name="urlId">The url id of the genre to get.</param>
        /// <returns>The genre model.</returns>
        public Genre? GetGenreByUrlId(string urlId);
    }
}
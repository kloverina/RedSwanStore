using System.Collections.Generic;
using System.Linq;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Repositories
{
    /// <summary>
    /// The class to work with database Genre table.
    /// </summary>
    public class GenreRepo : IGenreRepo
    {
        private readonly RedSwanStoreDBContent dbContent;

        public GenreRepo(RedSwanStoreDBContent dbContent)
        {
            this.dbContent = dbContent;
        }
        
        public IEnumerable<Genre> GetAllGenres()
        {
            IEnumerable<Genre> result = (
                from Genre genre in dbContent.Genres
                select genre
            );

            return result;
        }

        public Genre? GetGenreByName(string name)
        {
            Genre? result = dbContent.Genres.FirstOrDefault(
                g => g.Name == name
            );

            return result;
        }
    }
}
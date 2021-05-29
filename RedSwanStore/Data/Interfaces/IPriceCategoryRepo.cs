using System.Collections.Generic;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Interfaces
{
    /// <summary>
    /// An interface to work with database Price Category table.
    /// </summary>
    public interface IPriceCategoryRepo
    {
        /// <summary>
        /// Get all price categories from the table as collection.
        /// </summary>
        /// <returns>The collection of price category models.</returns>
        IEnumerable<PriceCategory> GetAllCategories();
        
        /// <summary>
        /// Get specified price category by its name.
        /// </summary>
        /// <param name="name">The name of the category to get.</param>
        /// <returns>The price category model.</returns>
        PriceCategory? GetCategoryByName(string name);


        /// <summary>
        /// Get specified price category by its url id.
        /// </summary>
        /// <param name="urlId">The url id of the category to get.</param>
        /// <returns>The price category model.</returns>
        public PriceCategory? GetCategoryByUrlId(string urlId);
    }
}
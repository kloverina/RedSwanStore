using System.Collections.Generic;
using System.Linq;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Repositories
{
    /// <summary>
    /// The class to work with database Price Category table.
    /// </summary>
    public class PriceCategoryRepo : IPriceCategoryRepo
    {
        private readonly RedSwanStoreDBContent dbContent;

        public PriceCategoryRepo(RedSwanStoreDBContent dbContent)
        {
            this.dbContent = dbContent;
        }
        
        public IEnumerable<PriceCategory> GetAllCategories()
        {
            IEnumerable<PriceCategory> result = (
                from PriceCategory priceCategory in dbContent.PriceCategories
                select priceCategory
            );

            return result;
        }

        public PriceCategory? GetCategoryByName(string name)
        {
            PriceCategory? result = dbContent.PriceCategories.FirstOrDefault(
                pc => pc.Name == name
            );

            return result;
        }
    }
}
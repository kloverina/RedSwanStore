using System.Collections.Generic;
using System.Linq;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Repositories
{
    /// <summary>
    /// The class to work with database Feature table.
    /// </summary>
    public class FeatureRepo : IFeatureRepo
    {
        private readonly RedSwanStoreDBContent dbContent;

        public FeatureRepo(RedSwanStoreDBContent dbContent)
        {
            this.dbContent = dbContent;
        }
        
        public IEnumerable<Feature> GetAllFeatures()
        {
            IEnumerable<Feature> result = (
                from Feature feature in dbContent.Features
                select feature
            );

            return result;
        }

        public Feature? GetFeatureByName(string name)
        {
            Feature? result = dbContent.Features.FirstOrDefault(
                f => f.Name == name
            );

            return result;
        }

        public Feature? GetFeatureByUrlId(string urlId)
        {
            Feature? result = dbContent.Features.FirstOrDefault(
                f => f.UrlId == urlId
            );

            return result;
        }
    }
}
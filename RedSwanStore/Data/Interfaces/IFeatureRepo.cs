using System.Collections;
using System.Collections.Generic;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Interfaces
{
    /// <summary>
    /// An interface to work with database Feature table.
    /// </summary>
    public interface IFeatureRepo
    {
        /// <summary>
        /// Get all features from the table as collection.
        /// </summary>
        /// <returns>The collection of feature models.</returns>
        public IEnumerable<Feature> GetAllFeatures();

        /// <summary>
        /// Get specified feature by its name.
        /// </summary>
        /// <param name="name">The name of the feature to get.</param>
        /// <returns>The feature model.</returns>
        public Feature? GetFeatureByName(string name);
    }
}
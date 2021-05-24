using System;
using System.Collections.Generic;
using System.Linq;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Repositories
{
    /// <summary>
    /// The class to work with database GameInfo table.
    /// </summary>
    public class GameInfoRepo : IGameInfoRepo
    {
        private readonly RedSwanStoreDBContent dbContent;

        public GameInfoRepo(RedSwanStoreDBContent dbContent)
        {
            this.dbContent = dbContent;
        }
        
        public void CheckAndUpdateDiscounts()
        {
            DateTime checkTime = new DateTime(
                DateTime.Now.Year, 
                DateTime.Now.Month, 
                DateTime.Now.Day, 
                18, 
                0, 
                0
            );
            
            if (DateTime.Now < checkTime)
                return;

            IEnumerable<GameInfo> updatedInfos = dbContent.GameInfos
                .Where(gi => gi.DiscountEndDate != DateTime.MinValue && gi.DiscountEndDate <= DateTime.Now);

            foreach (GameInfo updatedInfo in updatedInfos)
            {
                updatedInfo.Discount = 0.00f;
                updatedInfo.DiscountEndDate = DateTime.MinValue;
            }
            
            dbContent.GameInfos.UpdateRange(updatedInfos);
            dbContent.SaveChanges();
        }
    }
}
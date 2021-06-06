using System.Linq;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly RedSwanStoreDBContent dbContent;

        public CartRepo(RedSwanStoreDBContent dbContent)
        {
            this.dbContent = dbContent;
        }
        
        public void AddItem(string userEmail, string gameUrl)
        {
            CartModel? cart = dbContent.Cart.FirstOrDefault(ci => ci.UserEmail == userEmail);

            if (cart != null)
                dbContent.Cart.Remove(cart);

            dbContent.Cart.Add(new CartModel {
                UserEmail = userEmail,
                GameUrl = gameUrl
            });

            dbContent.SaveChanges();
        }

        public string? GetItemFor(string userEmail)
        {
            CartModel? cart = dbContent.Cart.FirstOrDefault(ci => ci.UserEmail == userEmail);

            return cart?.GameUrl;
        }

        public void DeleteItem(string userEmail)
        {
            CartModel? cart = dbContent.Cart.FirstOrDefault(ci => ci.UserEmail == userEmail);
            
            if (cart == null)
                return;

            dbContent.Cart.Remove(cart);
            dbContent.SaveChanges();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data
{
    public class RedSwanStoreDBContent : DbContext
    {
        public RedSwanStoreDBContent(DbContextOptions<RedSwanStoreDBContent> options) : base(options)
        {
            
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameInfo> GameInfos { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PriceCategory> PriceCategories { get; set; }
        public DbSet<GameFilter> GameFilters { get; set; }
        public DbSet<GameSystemRequirement> GameSystemRequirements { get; set; }
        public DbSet<GameMedia> GameMedias { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLibrary> UserLibraries { get; set; }
        public DbSet<UserLibraryGame> UserLibraryGames { get; set; }
    }
}
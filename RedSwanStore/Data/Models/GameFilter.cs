using System.Collections.Generic;

namespace RedSwanStore.Data.Models
{
    public class GameFilter
    {
        public int Id { get; set; }
        
        public int GameId { get; set; } // 'single to single' relation with Game
        public Game? Game { get; set; }
        
        // 'many to many' relation with Genre
        public List<Genre> Genres { get; set; } = new List<Genre>();
        
        // 'many to many' relation with Feature
        public List<Feature> Features { get; set; } = new List<Feature>();
    }
}
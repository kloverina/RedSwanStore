using System.Collections.Generic;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data.ViewModels
{
    public class GameManagementViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<Feature> Features { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
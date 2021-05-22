using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Data
{
    public class DBInitializer
    {
        private const string initialFiltersUrl = "Data\\DBInitialData\\InitialFilters.xml";
        private const string initialGamesUrl = "Data\\DBInitialData\\InitialGames.xml";
        private const string initialUsersUrl = "Data\\DBInitialData\\InitialUsers.xml";

        
        public static void Initialize(RedSwanStoreDBContent dbContent)
        {
            if (!dbContent.PriceCategories.Any())
                dbContent.PriceCategories.AddRange(InitializedPriceCategories);
            
            if (!dbContent.Features.Any())
                dbContent.Features.AddRange(InitializedFeatures);
            
            if (!dbContent.Genres.Any())
                dbContent.Genres.AddRange(InitializedGenres);

            if (!dbContent.GameMedias.Any())
            {
                dbContent.GameMedias.AddRange(
                    from Game game in InitializedGames
                    select game.GameMedia
                );
            }

            if (!dbContent.GameFilters.Any())
            {
                dbContent.GameFilters.AddRange(
                    from Game game in InitializedGames
                    select game.GameFilter
                );
            }

            if (!dbContent.GameSystemRequirements.Any())
            {
                dbContent.GameSystemRequirements.AddRange(
                    from Game game in InitializedGames
                    select game.GameSystemRequirements
                );
            }

            if (!dbContent.GameInfos.Any())
            {
                dbContent.GameInfos.AddRange(
                    from Game game in InitializedGames
                    select game.GameInfo
                );
            }
            
            if (!dbContent.Games.Any())
                dbContent.Games.AddRange(InitializedGames);
            
            if (!dbContent.Users.Any())
                dbContent.Users.AddRange(InitializedUsers);

            dbContent.SaveChanges();
        }

        
        private static List<PriceCategory> priceCategories;
        private static List<Feature> features;
        private static List<Genre> genres;
        private static List<Game> games;
        private static List<User> users;


        public static List<PriceCategory> InitializedPriceCategories {
            get {
                return priceCategories ??= XmlToModelParser.ParsePriceCategories(initialFiltersUrl);
            }
        }

        public static List<Feature> InitializedFeatures {
            get {
                return features ??= XmlToModelParser.ParseFeatures(initialFiltersUrl);
            }
        }

        public static List<Genre> InitializedGenres {
            get {
                return genres ??= XmlToModelParser.ParseGenres(initialFiltersUrl);
            }
        }

        public static List<Game> InitializedGames {
            get {
                genres ??= InitializedGenres;
                features ??= InitializedFeatures;
                
                return games ??= XmlToModelParser.ParseGames(initialGamesUrl, genres, features);
            }
        }
        
        public static List<User> InitializedUsers {
            get {
                return users ??= XmlToModelParser.ParseUsers(initialUsersUrl);
            }
        }

    }
}
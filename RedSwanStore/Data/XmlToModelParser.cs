using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using RedSwanStore.Data.Models;
using RedSwanStore.Utils;

namespace RedSwanStore.Data
{
    /// <summary>
    /// The wrapper for methods that allow to parse database initial data from XML
    /// to models entities that are used in database initializing.
    /// </summary>
    public class XmlToModelParser
    {
        private static GameInfo ParseGameInfo(XmlNode gameInfoNode)
        {
            var fieldsList = gameInfoNode.Cast<XmlNode>().ToList();
            
            var gameInfo = new GameInfo {
                Cover = fieldsList.First(
                    cn => cn.Name == nameof(GameInfo.Cover)
                ).InnerText,
                Price = Convert.ToDecimal(
                    fieldsList.GetNodeInnerText(nameof(GameInfo.Price)), CultureInfo.CurrentCulture
                ),
                Discount = (float)Convert.ToDouble(
                    fieldsList.GetNodeInnerText(nameof(GameInfo.Discount)), CultureInfo.CurrentCulture
                ),
                ReleaseDate = Convert.ToDateTime(
                    fieldsList.GetNodeInnerText(nameof(GameInfo.ReleaseDate)), CultureInfo.CurrentCulture
                ),
                Rating = Convert.ToByte(
                    fieldsList.GetNodeInnerText(nameof(GameInfo.Rating)), CultureInfo.CurrentCulture
                ),
                ShortDescription = fieldsList.GetNodeInnerText(nameof(GameInfo.ShortDescription)),
                DetailedDescription = fieldsList.GetNodeInnerXml(nameof(GameInfo.DetailedDescription)),
                LegalInfo = fieldsList.GetNodeInnerXml(nameof(GameInfo.LegalInfo))
            };

            return gameInfo;
        }


        private static GameSystemRequirement ParseGameSystemRequirement(XmlNode gameSystemRequirementNode)
        {
            var fieldsList = gameSystemRequirementNode.Cast<XmlNode>().ToList();

            var gameSystemRequirement = new GameSystemRequirement {
                MinCpu = fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.MinCpu)),
                MaxCpu = fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.MaxCpu)),
                MinRamMB = Convert.ToUInt32(
                    fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.MinRamMB)),
                    CultureInfo.CurrentCulture
                ),
                MaxRamMB = Convert.ToUInt32(
                    fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.MaxRamMB)),
                    CultureInfo.CurrentCulture
                ),
                DiskSpaceMB = Convert.ToUInt32(
                    fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.DiskSpaceMB)),
                    CultureInfo.CurrentCulture
                ),
                DirectX = Convert.ToByte(
                    fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.DirectX)),
                    CultureInfo.CurrentCulture
                ),
                MinGpu = fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.MinGpu)),
                MaxGpu = fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.MaxGpu)),
                SupportedOses = fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.SupportedOses)),
                ExtraInfo = fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.ExtraInfo)),
                SupportedVoiceLanguages = fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.SupportedVoiceLanguages)),
                SupportedTextLanguages = fieldsList.GetNodeInnerText(nameof(GameSystemRequirement.SupportedTextLanguages)),
            };

            return gameSystemRequirement;
        }


        private static GameMedia ParseGameMedia(XmlNode gameMediaNode)
        {
            var fieldsList = gameMediaNode.Cast<XmlNode>().ToList();
            
            var gameMedia = new GameMedia {
                Trailers = fieldsList.GetNodeInnerText(nameof(GameMedia.Trailers)),
                Screenshots = fieldsList.GetNodeInnerText(nameof(GameMedia.Screenshots))
            };

            return gameMedia;
        }


        private static GameFilter ParseGameFilter(XmlNode gameFilterNode, List<Genre> existingGenres, List<Feature> existingFeatures)
        {
            var fieldsList = gameFilterNode.Cast<XmlNode>().ToList();

            XmlNode genresNode = fieldsList.First(cn => cn.Name == nameof(GameFilter.Genres));
            List<Genre> genresList = (
                from XmlNode li in genresNode.ChildNodes 
                select existingGenres.First(g => g.Name == li.InnerText)
            ).ToList();

            XmlNode featuresNode = fieldsList.First(cn => cn.Name == nameof(GameFilter.Features));
            List<Feature> featuresList = (
                from XmlNode li in featuresNode.ChildNodes
                select existingFeatures.First(f => f.Name == li.InnerText)
            ).ToList();

            var gameFilter = new GameFilter {
                Genres = genresList,
                Features = featuresList
            };

            return gameFilter;
        }
        
        
        /// <summary>
        /// Create a list of Game models by parsing models data from the specified XML file. <para/>
        /// For correct database work, genres and features must be specified by its (already created)
        /// entities from the database.
        /// </summary>
        /// <param name="xmlFilePath">The path to the XML file containing data for Game models.</param>
        /// <param name="existingGenres">The initialized in database Genre models.</param>
        /// <param name="existingFeatures">The initialized in database Feature models.</param>
        /// <returns>The list of Game models parsed from the XML data.</returns>
        public static List<Game> ParseGames(string xmlFilePath, List<Genre> existingGenres, List<Feature> existingFeatures)
        {
            var document = new XmlDocument();
            document.Load(xmlFilePath);
            
            
            List<Game> gameModelsList = (
                from XmlNode gameNode in document.DocumentElement.ChildNodes
                select gameNode.ChildNodes.Cast<XmlNode>().ToList() into fieldsList
                select new Game {
                    Name = fieldsList.GetNodeInnerText(nameof(Game.Name)),
                    Developer = fieldsList.GetNodeInnerText(nameof(Game.Developer)),
                    GameInfo = ParseGameInfo(fieldsList.GetNode(nameof(GameInfo))),
                    GameSystemRequirements = ParseGameSystemRequirement(fieldsList.GetNode(nameof(GameSystemRequirement))),
                    GameMedia = ParseGameMedia(fieldsList.GetNode(nameof(GameMedia))),
                    GameFilter = ParseGameFilter(
                        fieldsList.GetNode(nameof(GameFilter)),
                        existingGenres, 
                        existingFeatures
                    )
            }).ToList();

            return gameModelsList;
        }


        
        /// <summary>
        /// Create a list of Feature models by parsing models data from the specified XML file.
        /// </summary>
        /// <param name="xmlFilePath">The path to the XML file containing data for filters.</param>
        /// <returns>The list of Feature models parsed from the XML data.</returns>
        public static List<Feature> ParseFeatures(string xmlFilePath)
        {
            var document = new XmlDocument();
            document.Load(xmlFilePath);
            
            XmlNode featuresRoot = document.DocumentElement.ChildNodes
                .Cast<XmlNode>()
                .ToList()
                .First(n => n.Name == nameof(GameFilter.Features));
            
            
            List<Feature> featuresList = (
                from XmlNode featureNode in featuresRoot
                select featureNode.ChildNodes.Cast<XmlNode>().ToList() into fieldsList
                select new Feature {
                    Name = fieldsList.GetNodeInnerText(nameof(Feature.Name))
                }
            ).ToList();

            return featuresList;
        }


        /// <summary>
        /// Create a list of Genre models by parsing models data from the specified XML file.
        /// </summary>
        /// <param name="xmlFilePath">The path to the XML file containing data for filters.</param>
        /// <returns>The list of Genre models parsed from the XML data.</returns>
        public static List<Genre> ParseGenres(string xmlFilePath)
        {
            var document = new XmlDocument();
            document.Load(xmlFilePath);
            
            XmlNode genresRoot = document.DocumentElement.ChildNodes
                .Cast<XmlNode>()
                .ToList()
                .First(
                    n => n.Name == nameof(GameFilter.Genres)
                );
            
            
            List<Genre> genresList = (
                from XmlNode genreNode in genresRoot
                select genreNode.ChildNodes.Cast<XmlNode>().ToList() into fieldsList
                select new Genre {
                    Name = fieldsList.GetNodeInnerText(nameof(Genre.Name))
                }
            ).ToList();

            
            return genresList;
        }


        /// <summary>
        /// Create a list of Price Category models by parsing models data from the specified XML file.
        /// </summary>
        /// <param name="xmlFilePath">The path to the XML file containing data for filters.</param>
        /// <returns>The list of Price Category models parsed from the XML data.</returns>
        public static List<PriceCategory> ParsePriceCategories(string xmlFilePath)
        {
            var document = new XmlDocument();
            document.Load(xmlFilePath);

            XmlNode priceCategoriesRoot = document.DocumentElement.ChildNodes
                .Cast<XmlNode>()
                .ToList()
                .First(
                    n => n.Name == "PriceCategories"
                );


            List<PriceCategory> priceCategoriesList = (
                from XmlNode priceCategoryNode in priceCategoriesRoot
                select priceCategoryNode.ChildNodes.Cast<XmlNode>().ToList() into fieldsList
                select new PriceCategory {
                    Name = fieldsList.GetNodeInnerText(nameof(PriceCategory.Name)),
                    MinPrice = Convert.ToDecimal(
                        fieldsList.GetNodeInnerText(nameof(PriceCategory.MinPrice)),
                        CultureInfo.CurrentCulture
                    ),
                    MaxPrice = Convert.ToDecimal(
                        fieldsList.GetNodeInnerText(nameof(PriceCategory.MaxPrice)),
                        CultureInfo.CurrentCulture
                    )
                }
            ).ToList();

            return priceCategoriesList;
        }

        /// <summary>
        /// Create a list of User models by parsing models data from the specified XML file.
        /// </summary>
        /// <param name="xmlFilePath">The path to the XML file containing data for users.</param>
        /// <returns>The list of User models parsed from the XML data.</returns>
        public static List<User> ParseUsers(string xmlFilePath)
        {
            var document = new XmlDocument();
            document.Load(xmlFilePath);
            
            List<User> usersList = (
                from XmlNode userNode in document.DocumentElement
                select userNode.ChildNodes.Cast<XmlNode>().ToList() into fieldsList
                select new User {
                    Name = fieldsList.GetNodeInnerText(nameof(User.Name)),
                    Surname = fieldsList.GetNodeInnerText(nameof(User.Surname)),
                    Login = fieldsList.GetNodeInnerText(nameof(User.Login)),
                    Password = fieldsList.GetNodeInnerText(nameof(User.Password)),
                    Photo = fieldsList.GetNodeInnerText(nameof(User.Photo)),
                    Balance = Convert.ToDecimal(
                        fieldsList.GetNodeInnerText(nameof(User.Balance)),
                        CultureInfo.CurrentCulture
                    )
                }
            ).ToList();

            return usersList;
        }
    }
}
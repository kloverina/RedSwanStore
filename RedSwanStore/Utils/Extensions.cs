﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Utils
{
    public static class Extensions
    {
        /// <summary>
        /// Get the xml node with specified name from the collection of xml nodes. 
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="nodeName">The name of the xml node to get.</param>
        /// <returns>The xml node having the same name as specified.</returns>
        public static XmlNode GetNode(this IEnumerable<XmlNode> lst, string nodeName)
        {
            return lst.First(n => n.Name == nodeName);
        }
        
        /// <summary>
        /// Get the inner text of the xml node with specified name.
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="nodeName">The name of the node to get the inner text of.</param>
        /// <returns>The inner text of the xml node having the same name as specified as string.</returns>
        public static string GetNodeInnerText(this IEnumerable<XmlNode> lst, string nodeName)
        {
            return lst.GetNode(nodeName).InnerText;
        }

        /// <summary>
        /// Get the inner xml of the xml node with specified name.
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="nodeName">The name of the node to get the inner xml of.</param>
        /// <returns>The inner xml of the xml node having the same name as specified as string.</returns>
        public static string GetNodeInnerXml(this IEnumerable<XmlNode> lst, string nodeName)
        {
            return lst.GetNode(nodeName).InnerXml;
        }

        
        /// <summary>
        /// Check whether filter of this game match the specified filter.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="matchFilter">The filter to check.</param>
        /// <returns>True if this game has the same filter as the specified one.</returns>
        public static bool IsMatchFilter(this Game game, GameFilter? matchFilter)
        {
            if (matchFilter is null)
                return true;
            
            
            var gameFilter = game.GameFilter;
            
            foreach (Feature feature in matchFilter.Features)
            {
                if (!gameFilter.Features.Exists(gf => gf.Name == feature.Name))
                    return false;
            }
            
            foreach (Genre genre in matchFilter.Genres)
            {
                if (!gameFilter.Genres.Exists(gg => gg.Name == genre.Name))
                    return false;
            }

            return true;
        }
        

        /// <summary>
        /// Check whether this game's price is in specified price category.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="category">The price category to check.</param>
        /// <returns>True if this game's price is in specified price category.</returns>
        public static bool IsMatchPriceCategory(this Game game, PriceCategory? category)
        {
            if (category is null)
                return true;
            
            var gamePrice = game.GameInfo.Price * (decimal)(1 - game.GameInfo.Discount);

            return gamePrice >= category.MinPrice && gamePrice <= category.MaxPrice;
        }


        /// <summary>
        /// Convert this decimal number to price (money) value for current culture
        /// and with currency sign.
        /// </summary>
        /// <param name="num"></param>
        /// <returns>Price string.</returns>
        public static string ConvertToPrice(this decimal num)
        {
            var stringed = decimal.Round(num, 2).ToString(CultureInfo.CurrentCulture);

            if (stringed.EndsWith(",00"))
                return $"{stringed.Substring(0, stringed.Length - 3)} ₽";
            
            return $"{stringed} ₽";
        }

        /// <summary>
        /// Convert this float number to percents value for current culture.
        /// </summary>
        /// <param name="num"></param>
        /// <returns>Percents string.</returns>
        public static string? ConvertToPercents(this float num)
        {
            if (num > 1)
                return "100%";

            if (num < 0)
                return null;

            return (num * 100).ToString(CultureInfo.CurrentCulture) + "%";
        }


        public static IEnumerable<Game> SortBy(this IEnumerable<Game> games, SortingTypes sortType)
        {
            IEnumerable<Game> result;
            
            switch (sortType)
            {
                case SortingTypes.Default:
                    result = games;
                    break;
                case SortingTypes.ReleaseDate:
                    result = games.OrderByDescending(g => g.GameInfo.ReleaseDate);
                    break;
                case SortingTypes.Alphabetically:
                    result = games.OrderBy(g => g.Name);
                    break;
                case SortingTypes.PriceDescending:
                    result = games.OrderByDescending(g => g.GameInfo.Price);
                    break;
                case SortingTypes.PriceAscending:
                    result = games.OrderBy(g => g.GameInfo.Price);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortType), sortType, null);
            }

            return result;
        }
    }
}
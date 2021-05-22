using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace RedSwanStore.Utils
{
    public static class Extensions
    {
        public static XmlNode GetNode(this IEnumerable<XmlNode> lst, string nodeName)
        {
            return lst.First(n => n.Name == nodeName);
        }
        
        public static string GetNodeInnerText(this IEnumerable<XmlNode> lst, string nodeName)
        {
            return lst.GetNode(nodeName).InnerText;
        }

        public static string GetNodeInnerXml(this IEnumerable<XmlNode> lst, string nodeName)
        {
            return lst.GetNode(nodeName).InnerXml;
        }
    }
}
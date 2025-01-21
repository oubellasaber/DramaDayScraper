using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace DramaDayScraper.Table.Cell.LinksGroup
{
    internal class LinksGroupParsingUtility
    {
        public static string[] ExtractLinkGroups(string cellHtml)
        {
            return cellHtml.Split("<br>", StringSplitOptions.RemoveEmptyEntries);
        }

        public static ICollection<ICollection<Link>> ParseAsLinks(string cellHtml)
        {
            var rawLinkGroups = ExtractLinkGroups(cellHtml);
            var scrapedLinkGroups = new List<ICollection<Link>>();

            foreach (var rawLinkGroup in rawLinkGroups)
                scrapedLinkGroups.Add(ExtractLinksFromGroup(rawLinkGroup));

            return scrapedLinkGroups;
        }

        public static ICollection<ICollection<Link>> ParseAsLinks(ICollection<HtmlNode> cells)
        {
            var rawLinkGroups = cells.Select(c => c.InnerHtml).ToArray();
            var scrapedLinkGroups = new List<ICollection<Link>>();

            foreach (var rawLinkGroup in rawLinkGroups)
                scrapedLinkGroups.Add(ExtractLinksFromGroup(rawLinkGroup));

            return scrapedLinkGroups;
        }

        private static ICollection<Link> ExtractLinksFromGroup(string linkGroupHtml)
            => HtmlNode.CreateNode($"<div>{linkGroupHtml}</div>")
                       .SelectNodes(".//a")
                       .Select(l => new Link
                       {
                           Host = l.InnerText.Trim(),
                           LinkUrl = l.GetAttributeValue("href", "")
                       })
            .ToList();
    }
}

using DramaDayScraper.Abstraction;
using DramaDayTransformer.Link.LinkCollection.Abstraction;
using HtmlAgilityPack;

namespace DramaDayTransformer.Link.LinkCollection.FileCrypt
{
    internal class FileCryptExtractor : IFileCryptExtractor
    {
        public static Result<IEnumerable<EpisodeLink>> Extract(HtmlDocument html)
        {
            HtmlNode htmlDoc = html.DocumentNode;
            List<EpisodeLink> episodeLinks = new();

            var rows = htmlDoc.SelectNodes(@".//table/tbody//tr");

            if (rows is null)
            {
                return Result.Failure<IEnumerable<EpisodeLink>>(new Error("FileCrypt.NoTable", "the html document has no table to parse"));
            }

            var onlineRows = rows.Where(n => n.SelectSingleNode(@"./[contains(./td[1]/i/@class, ""online"")]") != null);

            foreach (var row in onlineRows)
            {
                var titleCell = row.SelectSingleNode(".//td[@title]");
                var baseLinkNode = row.SelectSingleNode(".//a[@class='external_link']");
                var button = row.SelectSingleNode(".//button[@class='download']");

                if (titleCell == null || baseLinkNode == null || button == null)
                    continue;

                string title = titleCell.GetAttributeValue("title", "");
                string baseLink = baseLinkNode.GetAttributeValue("href", "");
                var dataAttribute = button.Attributes
                        .Where(attr => attr.Name.StartsWith("data-"))
                        .First().Value;

                episodeLinks.Add(new EpisodeLink(title, baseLink, dataAttribute));
            }

            return episodeLinks;
        }
    }
}

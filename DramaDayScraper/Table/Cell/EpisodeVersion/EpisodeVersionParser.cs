using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.LinksGroup;
using DramaDayScraper.Table.Cell.QualitiesGroup;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.EpisodeVersion
{
    internal class EpisodeVersionParser(QualitiesGroupParser qualitiesGroupParser, LinksGroupParser linksGroupParser)
        : IParser<IEnumerable<HtmlNode>, IEnumerable<EpisodeVersion>>
    {
        public IEnumerable<EpisodeVersion> Parse(IEnumerable<HtmlNode> input)
        {
            throw new NotImplementedException();
        }
    }
}

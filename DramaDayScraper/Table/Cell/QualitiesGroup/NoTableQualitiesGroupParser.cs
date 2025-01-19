using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.LinksGroup;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup
{
    internal class NoTableQualitiesGroupParser : IParser<IEnumerable<HtmlNode>, IEnumerable<Link>>
    {
        public IEnumerable<Link> Parse(IEnumerable<HtmlNode> input)
        {
            throw new NotImplementedException();
        }
    }
}

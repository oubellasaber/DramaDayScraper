using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup
{
    internal class NoTableLinksGroupParser : IParser<IEnumerable<HtmlNode>, IEnumerable<Link>>
    {
        public IEnumerable<Link> Parse(IEnumerable<HtmlNode> input)
        {
            throw new NotImplementedException();
        }
    }
}

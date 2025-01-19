using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup
{
    internal class TwoCellLinksGroupParser : IParser<IEnumerable<HtmlNode>, IEnumerable<Link>>
    {
        public IEnumerable<Link> Parse(IEnumerable<HtmlNode> input)
        {
            throw new NotImplementedException();
        }
    }
}

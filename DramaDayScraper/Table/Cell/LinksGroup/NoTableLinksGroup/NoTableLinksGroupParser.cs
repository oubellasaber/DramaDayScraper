using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup.NoTableLinksGroup
{
    internal class NoTableLinksGroupParser : IParser<IEnumerable<HtmlNode>, ICollection<ICollection<string>>>
    {
        public ICollection<ICollection<string>> Parse(IEnumerable<HtmlNode> input)
        {
            throw new NotImplementedException();
        }
    }
}

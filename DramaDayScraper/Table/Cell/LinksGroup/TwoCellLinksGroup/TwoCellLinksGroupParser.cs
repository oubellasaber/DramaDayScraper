using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup.TwoCellLinksGroup
{
    internal class TwoCellLinksGroupParser : IParser<IEnumerable<HtmlNode>, ICollection<ICollection<string>>>
    {
        ICollection<ICollection<string>> IParser<IEnumerable<HtmlNode>, ICollection<ICollection<string>>>.Parse(IEnumerable<HtmlNode> input)
        {
            throw new NotImplementedException();
        }
    }
}
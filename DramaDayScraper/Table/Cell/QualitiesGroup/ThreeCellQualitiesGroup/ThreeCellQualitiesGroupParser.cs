using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup.ThreeCellQualitiesGroup
{
    internal class ThreeCellQualitiesGroupParser : IParser<IEnumerable<HtmlNode>, ICollection<string>>
    {
        ICollection<string> IParser<IEnumerable<HtmlNode>, ICollection<string>>.Parse(IEnumerable<HtmlNode> input)
        {
            throw new NotImplementedException();
        }
    }
}

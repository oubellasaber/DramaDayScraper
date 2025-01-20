using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup.NoTableQualitiesGroup
{
    internal class NoTableQualitiesGroupParser : IParser<IEnumerable<HtmlNode>, ICollection<string>>
    {
        public ICollection<string> Parse(IEnumerable<HtmlNode> input)
        {
            throw new NotImplementedException();
        }
    }
}

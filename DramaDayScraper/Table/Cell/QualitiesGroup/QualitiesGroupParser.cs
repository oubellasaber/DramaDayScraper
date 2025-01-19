using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.LinksGroup;
using DramaDayScraper.Table.Cell.QualityLink.QualitiesGroup;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup
{
    internal class QualitiesGroupParser : IParser<IEnumerable<HtmlNode>, IEnumerable<Link>>
    {
        private readonly IParser<IEnumerable<HtmlNode>, IEnumerable<Link>> _qualitiesGroupParser;
        private readonly List<HtmlNode> _htmlNodes;

        public QualitiesGroupParser(List<HtmlNode> htmlNodes)
        {
            _htmlNodes = htmlNodes;

            _qualitiesGroupParser = htmlNodes.Count switch
            {
                _ when htmlNodes.All(n => n.OriginalName == "p") => new NoTableQualitiesGroupParser(),
                2 => new TwoCellQualitiesGroupParser(),
                3 => new ThreeCellQualitiesGroupParser(),
                _ => throw new ArgumentException($"Unsupported number of cells: {htmlNodes.Count}", nameof(htmlNodes))
            };
        }
        public IEnumerable<Link> Parse(IEnumerable<HtmlNode> input)
            => _qualitiesGroupParser.Parse(input);
    }
}

//using DramaDayScraper.Abstraction;
//using HtmlAgilityPack;

//namespace DramaDayScraper.Table.Cell.LinksGroup
//{
//    internal class LinksGroupParser : IParser<IEnumerable<HtmlNode>, IEnumerable<Link>>
//    {
//        private readonly IParser<IEnumerable<HtmlNode>, IEnumerable<Link>> _linksGroupParser;
//        private readonly List<HtmlNode> _htmlNodes;

//        public LinksGroupParser(List<HtmlNode> htmlNodes)
//        {
//            _htmlNodes = htmlNodes;

//            _linksGroupParser = htmlNodes.Count switch
//            {
//                _ when htmlNodes.All(n => n.OriginalName == "p") => new NoTableLinksGroupParser(),
//                2 => new TwoCellLinksGroupParser(),
//                3 => new ThreeCellLinksGroupParser(),
//                _ => throw new ArgumentException($"Unsupported number of cells: {htmlNodes.Count}", nameof(htmlNodes))
//            };
//        }

//        public IEnumerable<Link> Parse(IEnumerable<HtmlNode> input)
//            => _linksGroupParser.Parse(input);
//    }
//}

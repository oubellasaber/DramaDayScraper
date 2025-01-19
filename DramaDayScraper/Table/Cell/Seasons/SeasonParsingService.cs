using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Seasons
{
    internal class SeasonParsingService : ParsingService<HtmlNode, Season>
    {
        public SeasonParsingService(IEnumerable<ParserWithValidation<HtmlNode, Season>> parsersWithValidation)
            : base(parsersWithValidation)
        {
        }
    }
}

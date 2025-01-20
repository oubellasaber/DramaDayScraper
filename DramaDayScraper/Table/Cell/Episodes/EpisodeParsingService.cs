using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episodes
{
    internal class EpisodeParsingService : ParsingService<HtmlNode, Episode>
    {
        public EpisodeParsingService(IEnumerable<ParserWithValidation<HtmlNode, Episode>> parsersWithValidation) 
            : base(parsersWithValidation)
        {
        }
    }
}

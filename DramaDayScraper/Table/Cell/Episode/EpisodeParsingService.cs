using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episode
{
    internal class EpisodeParsingService : ParsingService<HtmlNode, Episode>
    {
        public EpisodeParsingService(IEnumerable<ParserWithValidation<HtmlNode, Episode>> parsersWithValidation) 
            : base(parsersWithValidation)
        {
        }
    }
}

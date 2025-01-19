using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.MediaVersions
{
    internal class MediaVersionParsingService : ParsingService<HtmlNode, MediaVersion>
    {
        public MediaVersionParsingService(
            IEnumerable<HtmlNodeParserWithValidation<MediaVersion>> parsersWithValidation)
            : base(parsersWithValidation)
        {
        }
    }
}

using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.MediaVersions
{
    internal class MediaVersionParserWithValidation : HtmlNodeParserWithValidation<MediaVersion>
    {
        public MediaVersionParserWithValidation(IValidator<HtmlNode, Result> validator, IParser<HtmlNode, Result<MediaVersion>> parser) : base(validator, parser)
        {
        }
    }
}

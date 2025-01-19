using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episode
{
    internal class EpisodeParserWithValidation : HtmlNodeParserWithValidation<Episode>
    {
        public EpisodeParserWithValidation(IValidator<HtmlNode, Result> validator, IParser<HtmlNode, Result<Episode>> parser) : base(validator, parser)
        {
        }
    }
}

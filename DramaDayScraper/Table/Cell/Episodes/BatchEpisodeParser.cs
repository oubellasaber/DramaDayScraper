using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episodes
{
    internal class BatchEpisodeParser : IParser<HtmlNode, Result<BatchEpisode>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<BatchEpisode> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<BatchEpisode> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, BatchEpisode>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episodes
{
    internal class SingleEpisodeParser : IParser<HtmlNode, Result<SingleEpisode>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<SingleEpisode> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<SingleEpisode> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, SingleEpisode>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

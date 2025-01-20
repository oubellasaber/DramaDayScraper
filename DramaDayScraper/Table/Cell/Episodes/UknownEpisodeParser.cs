using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episodes
{
    internal class UknownEpisodeParser : IParser<HtmlNode, Result<UknownEpisode>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<UknownEpisode> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<UknownEpisode> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, UknownEpisode>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

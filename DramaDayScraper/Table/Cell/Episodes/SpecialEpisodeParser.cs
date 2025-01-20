using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episodes
{
    internal class SpecialEpisodeParser : IParser<HtmlNode, Result<SpecialEpisode>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<SpecialEpisode> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<SpecialEpisode> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, SpecialEpisode>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

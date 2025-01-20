using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Pipeline;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Seasons
{
    internal class SeasonParser : IParser<HtmlNode, Result<Season>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<Season> Parse(HtmlNode input)
        {
            Season? season = null;

            season = Pipeline<Season?>
                .For(input, season)
                .Try(
                    parserValidator: HorizontalSeasonParser.ValidateAndParse,
                    onSuccess: (horizontalSeason, state) => state = horizontalSeason
                )
                .Try(
                    parserValidator: SidebarSeasonParser.ValidateAndParse,
                    onSuccess: (sidebarSeason, state) => state = sidebarSeason
                )
                .GetState();

            return ReferenceEquals(season, null)
                ? Result.Failure<Season>(Error.NoSuitableParserFound)
                : Result.Success<Season>(season);
        }

        public static Result<Season> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, Season>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Seasons
{
    internal class SidebarSeasonParser : IParser<HtmlNode, Result<Season>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<Season> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
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

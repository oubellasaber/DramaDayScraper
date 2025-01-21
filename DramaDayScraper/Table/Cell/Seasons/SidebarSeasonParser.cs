using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Validators;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace DramaDayScraper.Table.Cell.Seasons
{
    internal class SidebarSeasonParser : IParser<HtmlNode, Result<Season>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            var cellValidationResult = BaseHtmlTableCellValidator.Validate(input);
            if (cellValidationResult.IsFailure)
                return cellValidationResult;

            var tdNodes = input.SelectNodes(".//td");

            var secondCellText = tdNodes[0].InnerText;
            if (!Regex.IsMatch(tdNodes[0].InnerText, @"s[\w\s]*(\d{1,2})", RegexOptions.IgnoreCase))
                return Result.Failure(Error.MismatchedParser);

            return Result.Success();
        }

        public static Result<Season> Parse(HtmlNode input)
        {
            var season = new Season();

            season.SeasonNumber = int.Parse(
                Regex.Match(
                    input.SelectSingleNode("./td[1]").InnerText,
                    @"s[\w\s]*(\d{1,2})",
                    RegexOptions.IgnoreCase)
                .Groups[1].Value);

            return season;
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

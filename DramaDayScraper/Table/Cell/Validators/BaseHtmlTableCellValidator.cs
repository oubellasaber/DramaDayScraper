using DramaDayScraper.Abstraction;
using DramaDayScraper.Extentions;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Validators
{
    public class BaseHtmlTableCellValidator : IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            if (!input.IsTableRow())
                return Result.Failure(Error.HtmlNodeIsNotTr);

            var tdCount = input.SelectNodes(".//td")?.Count ?? -1;

            if (tdCount < 2)
            {
                return Result.Failure(Error.RowStructureIsNotSupported);
            }

            return Result.Success();
        }
    }
}

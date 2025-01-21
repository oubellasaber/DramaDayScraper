using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Validators;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup.TwoCellQualitiesGroup
{
    internal class TwoCellQualitiesGroupParser : IParser<HtmlNode, Result<ICollection<string>>>,
        IValidator<HtmlNode, Result>
    {
        public static Result<ICollection<string>> Parse(HtmlNode input)
        {
            var qualityGroups = new List<string>();
            var textNodes = input.SelectNodes(".//td")[1].SelectNodes(".//text()");

            if (textNodes != null)
            {
                foreach (var textNode in textNodes.ToList())
                {
                    if (textNode.InnerText.Contains(":"))
                    {
                        qualityGroups.Add(textNode.InnerText.Replace(":", "").Trim());
                        textNode.Remove();
                    }
                }
            }

            return qualityGroups;
        }

        public static Result Validate(HtmlNode input)
        {
            var qualityGroupsValidationResult = QualityGroupsValidator.Validate(input);
            if (qualityGroupsValidationResult.IsFailure)
            {
                return qualityGroupsValidationResult;
            }

            var nodes = input.SelectNodes(".//td");

            if (nodes.Count != 2)
                return Result.Failure(Error.NotExpectedFormat);

            return Result.Success();
        }

        public static Result<ICollection<string>> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, ICollection<string>>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

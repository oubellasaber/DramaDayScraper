﻿using Core.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Validators;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup.ThreeCellQualitiesGroup
{
    internal class ThreeCellQualitiesGroupParser : IParser<HtmlNode, Result<ICollection<string>>>,
        IValidator<HtmlNode, Result>
    {
        public static Result<ICollection<string>> Parse(HtmlNode input)
        {
            return input.SelectNodes(".//td")[1]
                .InnerHtml
                .Split("<br>", StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        public static Result Validate(HtmlNode input)
        {
            var qualityGroupsValidationResult = QualityGroupsValidator.Validate(input);
            if (qualityGroupsValidationResult.IsFailure)
            {
                return qualityGroupsValidationResult;
            }

            var nodes = input.SelectNodes(".//td");

            if (nodes.Count != 3)
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

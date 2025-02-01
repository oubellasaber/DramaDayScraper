﻿using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Validators;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup
{
    internal class ThreeCellLinksGroupParser : IParser<HtmlNode, Result<ICollection<ICollection<ShortLink>>>>,
        IValidator<HtmlNode, Result>
    {
        public static Result<ICollection<ICollection<ShortLink>>> Parse(HtmlNode input)
            => LinksGroupParsingUtility.ParseAsLinks(input.SelectNodes(".//td")[2].InnerHtml).ToList();

        public static Result Validate(HtmlNode input)
        {
            var linkGroupsValidationResult = LinkGroupsValidator.Validate(input);
            if (linkGroupsValidationResult.IsFailure)
            {
                return linkGroupsValidationResult;
            }

            var nodes = input.SelectNodes(".//td");

            if (nodes.Count != 3)
                return Result.Failure(Error.NotExpectedFormat);

            return Result.Success();
        }

        public static Result<ICollection<ICollection<ShortLink>>> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, ICollection<ICollection<ShortLink>>>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

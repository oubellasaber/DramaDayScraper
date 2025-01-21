﻿using DramaDayScraper.Abstraction;
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
            if (!string.IsNullOrEmpty(input.SelectSingleNode("./td[1]").InnerText))
                return Result.Failure(Error.MismatchedParser);

            return Result.Success();
        }

        public static Result<UknownEpisode> Parse(HtmlNode input)
        {
            return new UknownEpisode
            {
                Title = input.SelectSingleNode("./td[1]").InnerText
            };
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

﻿using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Episodes;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using DramaDayScraper.Table.Cell.EpisodeVersion;
using DramaDayScraper.Table.Pipeline;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell
{
    internal class EpisodeWithVersionsParser : IParser<HtmlNode, Result<Episode>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<Episode> Parse(HtmlNode input)
        {
            Episode? episode = null;

            episode = Pipeline<Episode?>
                .For(input, episode)
                .Try(
                    parserValidator: EpisodeParser.ValidateAndParse,
                    onSuccess: (episode, state) => state = episode,
                    isContinue: episode != null
                )
                .Try(
                    parserValidator: EpisodeVersionParser.ValidateAndParse,
                    onSuccess: (epVersions, state) => state!.EpisodeVersions = epVersions
                )
                .GetState();

            return ReferenceEquals(episode, null)
                ? Result.Failure<Episode>(Error.NoSuitableParserFound)
                : Result.Success<Episode>(episode);
        }

        public static Result<Episode> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, Episode>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

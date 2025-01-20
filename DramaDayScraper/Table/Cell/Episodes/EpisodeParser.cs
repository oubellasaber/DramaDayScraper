using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using DramaDayScraper.Table.Pipeline;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episodes
{
    internal class EpisodeParser : IParser<HtmlNode, Result<Episode>>,
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
                    parserValidator: SingleEpisodeParser.ValidateAndParse,
                    onSuccess: (singleEpisode, state) => state = singleEpisode
                )
                .Try(
                    parserValidator: SpecialEpisodeParser.ValidateAndParse,
                    onSuccess: (specialEpisode, state) => state = specialEpisode
                )
                .Try(
                    parserValidator: BatchEpisodeParser.ValidateAndParse,
                    onSuccess: (batchEpisode, state) => state = batchEpisode
                )
                .Try(
                    parserValidator: UknownEpisodeParser.ValidateAndParse,
                    onSuccess: (unknownEpisode, state) => state = unknownEpisode
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

using DramaDayScraper.Abstraction;
using DramaDayScraper.Extentions.Pipeline;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Episodes
{
    internal class EpisodeParsingHandler : IParser<HtmlNode, Result<Episode>>
    {
        public static Result<Episode> Parse(HtmlNode input)
        {
            ValueErrorState<Episode> episodeState = new();

            Pipeline<ValueErrorState<Episode>>
                .For(input, episodeState)
                .Try(
                    parserValidator: SingleEpisodeParser.ValidateAndParse,
                    onSuccess: (singleEpisode, state) => state.Value = singleEpisode,
                    onFailure: (result, state) => state.Error ??= result.Error
                )
                .Try(
                    parserValidator: SpecialEpisodeParser.ValidateAndParse,
                    onSuccess: (specialEpisode, state) => state.Value = specialEpisode,
                    onFailure: (result, state) => state.Error ??= result.Error
                )
                .Try(
                    parserValidator: BatchEpisodeParser.ValidateAndParse,
                    onSuccess: (batchEpisode, state) => state.Value = batchEpisode,
                    onFailure: (result, state) => state.Error ??= result.Error
                )
                .Try(
                    parserValidator: UknownEpisodeParser.ValidateAndParse,
                    onSuccess: (unknownEpisode, state) => state.Value = unknownEpisode,
                    onFailure: (result, state) => state.Error ??= result.Error
                );

            return ReferenceEquals(episodeState.Value, null) ?
                Result.Failure<Episode>(episodeState.Error!) :
                Result.Success<Episode>(episodeState.Value);
        }
    }
}

using DramaDayScraper.Abstraction;
using HtmlAgilityPack;
using DramaDayScraper.Table.Cell.Episodes;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using DramaDayScraper.Table.Cell.EpisodeVersion;
using DramaDayScraper.Extentions.Pipeline;

namespace DramaDayScraper.Table.Cell
{
    internal class EpisodeWithVersionsParsingHandler : IParser<HtmlNode, Result<Episode>>
    {
        public static Result<Episode> Parse(HtmlNode input)
        {
            ValueErrorState<Episode> episodeState = new();

            Pipeline<ValueErrorState<Episode>>
                .For(input, episodeState)
                .Try(
                    parserValidator: EpisodeParsingHandler.Parse,
                    onSuccess: (episode, state) => state.Value = episode,
                    onFailure: (result, state) => state.Error ??= result.Error,
                    isContinue: true
                )
                .Try(
                    parserValidator: EpisodeVersionsParsingHandler.Parse,
                    onSuccess: (episodeVersions, state) => state.Value!.EpisodeVersions = episodeVersions,
                    onFailure: (result, state) => state.Error ??= result.Error
                );

            return ReferenceEquals(episodeState.Value, null)
                ? Result.Failure<Episode>(episodeState.Error!)
                : Result.Success<Episode>(episodeState.Value);
        }
    }
}

using DramaDayScraper.Abstraction;
using DramaDayScraper.Extentions.Pipeline;
using DramaDayScraper.Table.Cell.LinksGroup;
using DramaDayScraper.Table.Cell.QualitiesGroup;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.EpisodeVersion
{
    internal class EpisodeVersionsParsingHandler : IParser<HtmlNode, Result<ICollection<EpVersion>>>
    {
        public static Result<ICollection<EpVersion>> Parse(HtmlNode input)
        {
            ValueErrorState<ICollection<ICollection<ShortLink>>> linkState = new();
            ValueErrorState<ICollection<string>> qualityState = new();

            // Pipeline to parse link groups
            Pipeline<ValueErrorState<ICollection<ICollection<ShortLink>>>>
                .For(input, linkState)
                .Try(
                    parserValidator: LinkGroupParsingHandler.Parse,
                    onSuccess: (linkGroups, state) => state.Value = linkGroups,
                    onFailure: (result, state) => state.Error = result.Error
                );

            // Pipeline to parse quality groups
            Pipeline<ValueErrorState<ICollection<string>>>
                .For(input, qualityState)
                .Try(
                    parserValidator: QualityParsingHandler.Parse,
                    onSuccess: (qualityGroups, state) => state.Value = qualityGroups,
                    onFailure: (result, state) => state.Error = result.Error
                );

            // Check if either parsing step failed
            if (ReferenceEquals(linkState.Value, null) || ReferenceEquals(qualityState.Value, null))
            {
                var error = linkState.Error ?? qualityState.Error;
                return Result.Failure<ICollection<EpVersion>>(error!);
            }

            // Combine the results from link groups and quality groups
            var epVersions = linkState.Value
                .Zip(qualityState.Value, (links, versionName) => new EpVersion
                {
                    EpisodeVerisonName = versionName,
                    Links = links
                })
                .ToList();

            return Result.Success<ICollection<EpVersion>>(epVersions);
        }
    }
}

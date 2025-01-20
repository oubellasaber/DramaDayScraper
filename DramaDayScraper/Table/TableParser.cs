using DramaDayScraper.Table.Cell;
using DramaDayScraper.Table.Cell.MediaVersions;
using DramaDayScraper.Table.Cell.Seasons;
using DramaDayScraper.Table.Pipeline;
using HtmlAgilityPack;
using System.Collections.ObjectModel;

namespace DramaDayScraper.Table
{
    internal class TableParser
    {
        public Collection<Season> Parse(HtmlNode table)
        {
            List<HtmlNode> nodes = new List<HtmlNode>();
            TablePipelineState state = new TablePipelineState();

            nodes.ForEach(node =>
            {
                Pipeline<TablePipelineState>
                .For(node, state)
                .Try(
                    parserValidator: SeasonParser.ValidateAndParse,
                    onSuccess: (season, state) => state.Seasons.Add(season),
                    isContinue: true
                )
                .EnsureExists(
                    condition: state => !state.Seasons.Any(),
                    action: state => state.Seasons.Add(new Season { SeasonNumber = 1 })
                )
                .Try(
                     parserValidator: MediaVersionParser.ValidateAndParse,
                     onSuccess: (version, state) => state.CurrentSeason?.MediaVersions.Add(version),
                     isContinue: true
                )
                .EnsureExists(
                    condition: state => state.CurrentSeason != null && !state.CurrentSeason.MediaVersions.Any(),
                    action: state => state.CurrentSeason?.MediaVersions.Add(new MediaVersion() { MediaVersionName = "default" })
                )
                .Try(
                    parserValidator: EpisodeWithVersionsParser.ValidateAndParse,
                    onSuccess: (episode, state) => state.CurrentMediaVersion?.EpisodeVersions.Add(episode)
                );
            });

            return state.Seasons;
        }
    }
}

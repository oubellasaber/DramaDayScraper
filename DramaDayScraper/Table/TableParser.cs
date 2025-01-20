using DramaDayScraper.Pipeline;
using DramaDayScraper.Table.Cell;
using DramaDayScraper.Table.Cell.Episodes;
using DramaDayScraper.Table.Cell.MediaVersions;
using DramaDayScraper.Table.Cell.Seasons;
using HtmlAgilityPack;
using System.Collections.ObjectModel;

namespace DramaDayScraper.Table
{
    internal class TableParser
    {
        private readonly SeasonParsingService _seasonParsingService;
        private readonly MediaVersionParsingService _mediaVersionParsingService;
        private readonly EpisodeWithVersionsParser _episodeWithVersionsParser;

        public TableParser(
            SeasonParsingService seasonParsingService,
            MediaVersionParsingService mediaVersionParsingService,
            EpisodeWithVersionsParser episodeWithVersionsParser)
        {
            _seasonParsingService = seasonParsingService;
            _mediaVersionParsingService = mediaVersionParsingService;
            _episodeWithVersionsParser = episodeWithVersionsParser;
        }

        public Collection<Season> Parse(HtmlNode table)
        {
            List<HtmlNode> nodes = new List<HtmlNode>();
            TablePipelineState state = new TablePipelineState();

            nodes.ForEach(node =>
            {
                Pipeline<TablePipelineState>
                .For(node, state)
                .Try<Season>(
                    parser: node => _seasonParsingService.ValidateAndParse(node),
                    onSuccess: (season, state) => state.Seasons.Add(season),
                    isContinue: true
                )
                .EnsureExists(
                    condition: state => !state.Seasons.Any(),
                    action: state => state.Seasons.Add(new Season { SeasonNumber = 1 })
                )
                .Try<MediaVersion>(
                     parser: node => _mediaVersionParsingService.ValidateAndParse(node),
                     onSuccess: (version, state) => state.CurrentSeason?.MediaVersions.Add(version),
                     isContinue: true
                )
                .EnsureExists(
                    condition: state => state.CurrentSeason != null && !state.CurrentSeason.MediaVersions.Any(),
                    action: state => state.CurrentSeason?.MediaVersions.Add(new MediaVersion() { MediaVersionName = "default" })
                )
                .Try<Episode>(
                    parser: node => _episodeWithVersionsParser.ValidateAndParse(node),
                    onSuccess: (episode, state) => state.CurrentMediaVersion?.EpisodeVersions.Add(episode)
                );
            });

            return state.Seasons;
        }
    }
}

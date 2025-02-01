using DramaDayScraper.Extentions;
using DramaDayScraper.Extentions.Pipeline;
using DramaDayScraper.Table.Cell;
using DramaDayScraper.Table.Cell.MediaVersions;
using DramaDayScraper.Table.Cell.Seasons;
using HtmlAgilityPack;
using System.Collections.ObjectModel;

namespace DramaDayScraper.Table
{
    internal class TableParser
    {
        public static Collection<Season> Parse(HtmlNode table)
        {
            List<HtmlNode> nodes = table.SelectNodes(".//tr").ToList();
            RemoveUncesseryRowsFromTable(nodes);
            TablePipelineState state = new TablePipelineState();

            nodes.ForEach(node =>
            {
                Pipeline<TablePipelineState>
                .For(node, state)
                .Try(
                    parserValidator: SeasonParsingHandler.Parse,
                    onSuccess: (season, state) => state.Seasons.Add(season),
                    isContinue: true
                )
                .EnsureExists(
                    condition: state => !state.Seasons.Any(),
                    action: state => state.Seasons.Add(new Season { SeasonNumber = 1 })
                )
                .Try(
                    parserValidator: MediaVersionParsingHandler.Parse,
                    onSuccess: (version, state) => state.CurrentSeason?.MediaVersions.Add(version),
                    isContinue: true
                )
                .EnsureExists(
                    condition: state => state.CurrentSeason != null && !state.CurrentSeason.MediaVersions.Any(),
                    action: state => state.CurrentSeason?.MediaVersions.Add(new MediaVersion() { MediaVersionName = "default" })
                )
                .Try(
                    parserValidator: EpisodeWithVersionsParsingHandler.Parse,
                    onSuccess: (episode, state) => state.CurrentMediaVersion?.Episodes.Add(episode)
                )
                .Finally(
                    finalAction: ctx => Console.WriteLine($"Log {ctx.Node.InnerHtml}")
                 );
            });

            return state.Seasons;
        }

        private static void RemoveUncesseryRowsFromTable(List<HtmlNode> rows)
        {
            if (rows[0].IsHeader())
            {
                rows.RemoveAt(0);
            }

            rows.RemoveAll(r => r.IsEmptyRow() || r.IsPasswordRow());
        }
    }
}

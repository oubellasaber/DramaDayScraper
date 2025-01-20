using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Episodes;
using DramaDayScraper.Table.Cell.EpisodeVersion;
using HtmlAgilityPack;
using System.Runtime.InteropServices;

namespace DramaDayScraper.Table.Cell
{
    internal class EpisodeWithVersionsParser : IParser<HtmlNode, Result<Episode>>
    {
        private readonly EpisodeParsingService _episodeParsingService;
        private readonly EpisodeVersionParsingService _episodeVersionParsingService;

        public EpisodeWithVersionsParser(
            EpisodeParsingService episodeParsingService, 
            EpisodeVersionParsingService episodeVersionParsingService)
        {
            _episodeParsingService = episodeParsingService;
            _episodeVersionParsingService = episodeVersionParsingService;
        }

        public Result<Episode> ValidateAndParse(HtmlNode input)
        {
            // use the episodeparsingservice then the episodeversionparsing
        }
    }
}

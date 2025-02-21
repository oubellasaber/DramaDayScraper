using Core.Abstraction;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using DramaDayScraper.Table.Cell.MediaVersions;
using DramaDayScraper.Table.Cell.Seasons;
using System.Collections.ObjectModel;

namespace DramaDayScraper.Table
{
    public class TablePipelineState
    {
        public Error? Error { get; set; }
        public Collection<Season> Seasons { get; } = new();
        public Season? CurrentSeason => Seasons.LastOrDefault();
        public MediaVersion? CurrentMediaVersion => CurrentSeason?.MediaVersions.LastOrDefault();
        public Episode? CurrentEpisode => CurrentMediaVersion?.Episodes.LastOrDefault();
    }
}
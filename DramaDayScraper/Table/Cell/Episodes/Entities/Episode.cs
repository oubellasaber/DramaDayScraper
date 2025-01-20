using DramaDayScraper.Table.Cell.EpisodeVersion;

namespace DramaDayScraper.Table.Cell.Episodes.Entities
{
    public class Episode
    {
        public ICollection<EpVersion> EpisodeVersions { get; set; } = new List<EpVersion>();
    }
}

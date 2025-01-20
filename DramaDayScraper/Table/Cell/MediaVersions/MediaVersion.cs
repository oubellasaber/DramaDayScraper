using DramaDayScraper.Table.Cell.Episodes;

namespace DramaDayScraper.Table.Cell.MediaVersions
{
    public class MediaVersion
    {
        public string MediaVersionName { get; set; }

        public ICollection<Episode> EpisodeVersions { get; set; } = new List<Episode>();
    }
}

using DramaDayScraper.Table.Cell.Episodes.Entities;

namespace DramaDayScraper.Table.Cell.MediaVersions
{
    public class MediaVersion
    {
        public string MediaVersionName { get; set; }

        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    }
}

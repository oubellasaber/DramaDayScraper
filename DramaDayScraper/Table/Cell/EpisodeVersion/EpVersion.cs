using DramaDayScraper.Table.Cell.LinksGroup;

namespace DramaDayScraper.Table.Cell.EpisodeVersion
{
    public class EpVersion
    {
        public string EpisodeVerisonName { get; set; }

        public ICollection<Link> Links { get; set; } = new List<Link>();
    }
}

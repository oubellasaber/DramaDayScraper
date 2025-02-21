using DramaDayScraper.Table.Cell.Seasons;
using System.Collections.ObjectModel;

namespace DramaDayScraper
{
    internal class Media
    {
        public string DramaDayId { get; set; }
        public string? KrTitle { get; set; }
        public string EnTitle { get; set; }
        public IEnumerable<Season> Seasons { get; set; } = new List<Season>();
    }
}
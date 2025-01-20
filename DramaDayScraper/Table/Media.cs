using DramaDayScraper.Table.Cell.Seasons;
using System.Collections.ObjectModel;

namespace DramaDayScraper.Table
{
    internal class Media
    {
        public ICollection<Season> Seasons { get; set; } = new Collection<Season>();
    }
}
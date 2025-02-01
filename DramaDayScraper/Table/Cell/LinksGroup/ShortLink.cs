namespace DramaDayScraper.Table.Cell.LinksGroup
{
    public class ShortLink
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Host { get; set; }
        public string LinkUrl { get; set; }
    }
}

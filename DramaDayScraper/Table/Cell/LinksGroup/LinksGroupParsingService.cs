using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup
{
    internal class LinksGroupParsingService : ParsingService<HtmlNode, ICollection<ICollection<string>>>
    {
        public LinksGroupParsingService(IEnumerable<ParserWithValidation<HtmlNode, ICollection<ICollection<string>>>> parsersWithValidation) : base(parsersWithValidation)
        {
        }
    }
}
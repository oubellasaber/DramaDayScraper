using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup
{
    internal class QualitiesGroupParsingService : ParsingService<HtmlNode, ICollection<string>>
    {
        public QualitiesGroupParsingService(IEnumerable<ParserWithValidation<HtmlNode, ICollection<string>>> parsersWithValidation) 
            : base(parsersWithValidation)
        {
        }
    }
}

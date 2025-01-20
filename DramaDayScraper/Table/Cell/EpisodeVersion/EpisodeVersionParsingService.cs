using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.LinksGroup;
using DramaDayScraper.Table.Cell.QualitiesGroup;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.EpisodeVersion
{
    internal class EpisodeVersionParsingService : IParser<HtmlNode, IEnumerable<EpVersion>>
    {
        private readonly QualitiesGroupParsingService _qualitiesGroupParsingService;
        private readonly LinksGroupParsingService _linksGroupParsingService;

        public EpisodeVersionParsingService(QualitiesGroupParsingService qualitiesGroupParsingService, 
            LinksGroupParsingService linksGroupParsingService)
        {
            _qualitiesGroupParsingService = qualitiesGroupParsingService;
            _linksGroupParsingService = linksGroupParsingService;
        }

        public IEnumerable<EpVersion> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
        }
    }
}

using DramaDayScraper.Abstraction;
using DramaDayTransformer.Link.LinkCollection.FileCrypt;
using HtmlAgilityPack;

namespace DramaDayTransformer.Link.LinkCollection.Abstraction
{
    public interface IFileCryptExtractor
    {
        abstract static Result<IEnumerable<EpisodeLink>> Extract(HtmlDocument html);
    }
}
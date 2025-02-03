using DramaDayScraper.Abstraction;
using HttpRequestHeaders = System.Collections.Generic.IEnumerable<(string Key, string Value)>;

namespace DramaDayTransformer.Link.LinkCollection.Abstraction
{
    public interface IFileCryptLinkResolver
    {
        abstract static Task<Result<string>> Resolve(string url, string host, HttpRequestHeaders headers);
    }
}

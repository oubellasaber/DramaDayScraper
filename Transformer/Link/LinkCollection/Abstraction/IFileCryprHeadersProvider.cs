using DramaDayScraper.Abstraction;
using System.Net.Http.Headers;

namespace DramaDayTransformer.Link.LinkCollection.Abstraction
{
    public interface IFileCryprHeadersProvider
    {
        abstract static Result<IEnumerable<(string Key, string Value)>> GetHeaders(HttpResponseHeaders responseHeaders);
    }
}

using Core.Abstraction;
using HtmlAgilityPack;

namespace DramaDayTransformer
{
    public interface IHtmlFetcher
    {
        Task<Result<HtmlDocument>> FetchAsync(string url);
    }

}

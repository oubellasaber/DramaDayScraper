using DramaDayScraper.Abstraction;

namespace DramaDayTransformer.Link.LinkCollection.Abstraction
{
    public interface IScraperApiClient
    {
        Task<Result<HttpResponseMessage>> ScrapeHtml(string url);
        Task<Result<HttpResponseMessage>> ScrapeFileCryptContainer(string containerUrl, string? password = default);
    }
}
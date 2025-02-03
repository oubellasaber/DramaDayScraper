using DramaDayScraper.Abstraction;
using DramaDayTransformer.Link.LinkCollection.Abstraction;
using HtmlAgilityPack;

namespace DramaDayTransformer.Link.LinkCollection.FileCrypt
{
    public class FileCryptProvider
    {
        private readonly IScraperApiClient _scraperApi;
        private readonly HttpClient _client;

        public FileCryptProvider(IScraperApiClient scraperApi, HttpClient client)
        {
            _scraperApi = scraperApi;
            _client = client;
        }

        public async Task<Result<IEnumerable<EpisodeLink>>> GetEpisodeLinks(string url)
        {
            if (!url.Contains("filecrypt"))
            {
                return Result.Failure<IEnumerable<EpisodeLink>>(new Error("FileCrypt.WrongUrl", "The provided url is not supported"));
            }

            var response = await _scraperApi.ScrapeFileCryptContainer(url);

            if (response.IsFailure)
                return Result.Failure<IEnumerable<EpisodeLink>>(response.Error);

            var headers = FileCryprHeadersProvider.GetHeaders(response.Value.Headers);

            if (headers.IsFailure)
                return Result.Failure<IEnumerable<EpisodeLink>>(headers.Error);

            var html = await response.Value.Content.ReadAsStringAsync();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var episodeLinks = FileCryptExtractor.Extract(htmlDoc);

            if (episodeLinks.IsFailure)
                return Result.Failure<IEnumerable<EpisodeLink>>(episodeLinks.Error);

            var tasks = episodeLinks.Value.Select(async epLink =>
            {
                var url = $"https://filecrypt.co/Link/{epLink.DataAttributeForLink}.html";

                var result = await FileCryptResolver.Resolve(url, epLink.BaseLink, headers.Value);

                if (result.IsFailure)
                {
                    // Implement a retry
                    // Log what went wrong
                }
                else
                {
                    epLink.DirectLink = result.Value;
                }
            });

            await Task.WhenAll(tasks);

            return episodeLinks;
        }
    }
}

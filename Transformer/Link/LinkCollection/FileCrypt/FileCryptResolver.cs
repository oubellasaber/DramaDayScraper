using DramaDayScraper.Abstraction;
using DramaDayTransformer.Link.LinkCollection.Abstraction;
using System.Text.RegularExpressions;
using HttpRequestHeaders = System.Collections.Generic.IEnumerable<(string Key, string Value)>;

namespace DramaDayTransformer.Link.LinkCollection.FileCrypt
{
    public class FileCryptResolver : IFileCryptLinkResolver
    {
        public static async Task<Result<string>> Resolve(string url, string host, HttpRequestHeaders headers)
        {
            var response = await GetResponse(url, headers, new HttpClientHandler { AllowAutoRedirect = true });
            string redirectHtml = await response.Content.ReadAsStringAsync();

            Regex regex = new Regex(@"href='(?<redirect>[^']*)'");

            Match match = regex.Match(redirectHtml);

            if (!match.Success)
                return Result.Failure<string>(new Error("FileCrypt.HtmlHasNoRedirectUrl", "The returned html does not contain the redirect url"));

            string redirectUrl = match.Groups["redirect"].Value;

            var finalResponse = await GetResponse(redirectUrl, headers, new HttpClientHandler { AllowAutoRedirect = !host.Contains("datanodes") });

            return host.Contains("datanodes")
                ? finalResponse?.Headers?.Location?.ToString()
                : finalResponse?.RequestMessage?.RequestUri?.ToString();
        }

        private static async Task<HttpResponseMessage> GetResponse(string url, HttpRequestHeaders headers, HttpClientHandler httpHandler)
        {
            var client = new HttpClient(httpHandler);

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            foreach (var h in headers)
                request.Headers.Add(h.Key, h.Value);

            var response = await client.SendAsync(request);

            return response;
        }
    }
}

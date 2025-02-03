using DramaDayScraper.Abstraction;
using DramaDayTransformer.Link.LinkCollection.Abstraction;
using System.Net.Http.Headers;

namespace DramaDayTransformer.Link.LinkCollection.FileCrypt
{
    internal class FileCryprHeadersProvider : IFileCryprHeadersProvider
    {
        public static Result<IEnumerable<(string Key, string Value)>> GetHeaders(HttpResponseHeaders responseHeaders)
        {
            var phpSessionHeader = GetPhpSessionHeader(responseHeaders);

            if (phpSessionHeader.IsFailure)
                return Result.Failure<IEnumerable<(string, string)>>(phpSessionHeader.Error);

            (string, string)[] headers = [
                ("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36"),
                ("Cookie", phpSessionHeader.Value)
                ];

            return headers;
        }

        private static Result<string> GetPhpSessionHeader(HttpResponseHeaders headers)
        {
            if (!headers.TryGetValues("Set-Cookie", out var cookies))
                return Result.Failure<string>(new Error("FileCrypt.Cookies", "Set-Cookie header not found"));

            var sessionCookie = cookies.FirstOrDefault(cookie => cookie.Contains("PHPSESSID="));
            if (sessionCookie == null)
                return Result.Failure<string>(new Error("FileCrypt.Cookies", "PHPSESSID cookie not found"));

            var phpSession = sessionCookie
                .Split(';')
                .First(part => part.Contains("PHPSESSID="));

            return phpSession;
        }
    }
}

using HtmlAgilityPack;
using LinkTransformer.AutoLinkResolution.Shared;
using System.Text.RegularExpressions;

namespace LinkTransformer.AutoLinkResolution
{
    // needs a logger and IConfiguration
    public class DramaDayResolver
    {
        public static async Task<string?> ResolveLink(string link)
        {
            try
            {
                var firstRedirect = await Utility.GetFirstRedirectAsync(link);

                if (firstRedirect == null)
                    return null;

                HttpClient client = new HttpClient();
                var redirectResponse = await client.GetAsync(firstRedirect);

                if (!redirectResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Response did not success, Status code {redirectResponse.StatusCode}, Link {link}");
                    return null;
                }

                var html = await redirectResponse.Content.ReadAsStringAsync();
                var requetParameters = ExtractTokenAndAction(html);

                if (!requetParameters.HasValue)
                {
                    Console.WriteLine($"Could not extract link parameters from the html from {link}");
                    return null;
                }

                var directLink = await MakePostRequestAndGetLocationAsync(requetParameters.Value.token, requetParameters.Value.action);

                if (directLink is null)
                    return null;

                return directLink;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrng: {ex.Message}");
                return null;
            }
        }

        static async Task<string?> MakePostRequestAndGetLocationAsync(string token, string action)
        {
            string url = "https://riviwi.com/wp-admin/admin-ajax.php"; // use config for this, domain may change in the future

            using HttpClientHandler handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            using (HttpClient client = new HttpClient(handler))
            {
                var parameters = new Dictionary<string, string>
                {
                    ["token"] = token.Replace("\\/", "/"),
                    ["action"] = action
                };

                var content = new FormUrlEncodedContent(parameters);

                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.Headers.Contains("Location"))
                    {
                        return response.Headers.Location!.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Headers does not contains a location");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while trying to retrieve response location: {ex.Message}");
                }
            }

            return null;
        }

        static (string token, string action)? ExtractTokenAndAction(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var scriptNode = doc.DocumentNode.SelectSingleNode("//script");

            if (scriptNode != null)
            {
                string scriptContent = scriptNode.InnerHtml;

                string tokenPattern = @"""token"":""([^""]+)\""";
                string token = Regex.Match(scriptContent, tokenPattern).Groups[1].Value;

                string actionPattern = @"""soralink_z"":""([^""]+)\""";
                string action = Regex.Match(scriptContent, actionPattern).Groups[1].Value;

                return (token, action);
            }

            return null;
        }
    }
}

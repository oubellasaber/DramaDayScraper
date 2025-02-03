using DramaDayScraper.Table;
using DramaDayTransformer.Link.AutoLinkResolution;
using DramaDayTransformer.Link.LinkCollection;
using HtmlAgilityPack;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

public class Program
{
    public static void Mai1n()
    {
        f().GetAwaiter().GetResult();
    }
    public async static Task f()
    {
        string url = "https://dramaday.me/?6bb2feb0ae=VElyUGlTemJNUk1Pb1NsNU9Pc3p5M0tFTUMzNW1ORTY5VkxZenIxbk9nd21EcXlpNG1ERXBpZVcxOWR4Zkc5c0xabUNCd09ucU9NRy80SHFmUXFBcndVclNXTDVpYXRHalQ1azlLWHc2Q01QMzJyYm1WZ2FtRlRFZzlFV1pnLzY="; // Replace with your URL

        // Create an HttpClient instance
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Fetch the HTML content
                string htmlContent = await client.GetStringAsync(url);

                // Extract the JSON for "item" and "options" using regex
                string jsonItem = ExtractJsonWithRegex(htmlContent, "item");
                string jsonOptions = ExtractJsonWithRegex(htmlContent, "options");

                if (string.IsNullOrEmpty(jsonItem) || string.IsNullOrEmpty(jsonOptions))
                {
                    Console.WriteLine("JSON data not found in the HTML.");
                    return;
                }

                // Parse the JSON data
                var item = JsonSerializer.Deserialize<Item>(jsonItem);
                var options = JsonSerializer.Deserialize<Options>(jsonOptions);

                // Construct the URL-encoded string
                string result = $"token={HttpUtility.UrlEncode(item.token)}" +
                               $"&id={HttpUtility.UrlEncode(item.id.ToString())}" +
                               $"&time={HttpUtility.UrlEncode(item.time.ToString())}" +
                               $"&post={HttpUtility.UrlEncode(item.post)}" +
                               $"&redirect={HttpUtility.UrlEncode(item.redirect)}" +
                               $"&cacha={HttpUtility.UrlEncode(item.cacha)}" +
                               //$"&new={HttpUtility.UrlEncode(item.New.ToString().ToLower())}" +
                               $"&link={HttpUtility.UrlEncode(item.link)}" +
                               $"&action={HttpUtility.UrlEncode(options.soralink_z)}";

                // Print the result
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Helper method to extract JSON using regex
    static string ExtractJsonWithRegex(string htmlContent, string variableName)
    {
        // Regex pattern to match the JSON object
        string pattern = @$"{variableName} = (?<json>{{.*?}})";
        Match match = Regex.Match(htmlContent, pattern, RegexOptions.Singleline);

        // Return the JSON if found, otherwise an empty string
        return match.Success ? match.Groups["json"].Value : string.Empty;
    }

    // Class to represent the "item" JSON object
    class Item
    {
        public string token { get; set; }
        public int id { get; set; }
        public long time { get; set; }
        public string post { get; set; }
        public string redirect { get; set; }
        public string cacha { get; set; }
        public string link { get; set; }
    }

    // Class to represent the "options" JSON object
    class Options
    {
        public string soralink_z { get; set; }
    }
    public static void Maisn()
    {
        var htmlWeb = new HtmlWeb();
        var htmlDoc = htmlWeb.LoadFromWebAsync("https://dramaday.me/sorry-not-sorry/").Result;
        var tbody = htmlDoc.DocumentNode
            .SelectSingleNode(".//tbody");

        var result = TableParser.Parse(tbody);

        Console.WriteLine(JsonSerializer.Serialize(result));

        Console.WriteLine();
    }

    private static async Task<string?> GetRedirectUrl(string url, string host)
    {
        List<(string key, string value)> headers = new();

        headers.Add(("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36"));
        headers.Add(("Cookie", "lang=en; PHPSESSID=0lndcd934e0acbon5nt7drlo47; ab=1732121426; ha=1; haad=1; haac=1; ac=2; acn=2"));


        var response = await GetResponse(url, headers, new HttpClientHandler { AllowAutoRedirect = true });
        string redirectHtml = await response.Content.ReadAsStringAsync();

        Regex regex = new Regex(@"href='(?<redirect>[^']*)'");

        Match match = regex.Match(redirectHtml);

        if (!match.Success)
            return null;

        string redirectUrl = match.Groups["redirect"].Value;

        var finalResponse = await GetResponse(redirectUrl, headers, new HttpClientHandler { AllowAutoRedirect = !host.Contains("datanodes") });

        return host.Contains("datanodes") ? finalResponse?.Headers?.Location?.ToString() : finalResponse?.RequestMessage?.RequestUri?.ToString();
    }

    private static async Task<HttpResponseMessage?> GetResponse(string url, IEnumerable<(string key, string value)> headers, HttpClientHandler httpHandler)
    {
        var client = new HttpClient(httpHandler);
        var currentUrl = url;

        var request = new HttpRequestMessage(HttpMethod.Get, currentUrl);
        foreach (var h in headers)
        {
            request.Headers.Add(h.key, h.value);
        }

        var response = await client.SendAsync(request);

        return response;
    }

    public static async Task Main()
    {
        //var result = await L4sResolver.ResolveLink("https://l4s.cc/JBc3");
        //var result = await FileCrypt.GetRedirectUrl("https://filecrypt.co/Link/3vhcQ2aYby_W0jwPS9tsWWgso9J7LH5A89Izlw68hryd4Xwtqsq14LTBPugp2PjY4fEc2aLSjBIZ3TF2wWgPfUkXAH0vH-odNtpT6xpUnySNMt_EFIUGnkx8vbp42-1g.html", "datanodes");
        
        //Console.WriteLine(result!);

        HttpClient httpClient = new HttpClient();

        string apiKey = "c0059d9caa18e3b31252f1622aef9590";
        string fileCryptContainerUrl = "https://filecrypt.co/Container/B3CFD96DB8.html";

        HttpResponseMessage result = await httpClient.GetAsync($"http://api.scraperapi.com/?api_key={apiKey}&url={fileCryptContainerUrl}&premium=true");

        foreach (var item in result.Headers.GetValues("Set-Cookie"))
        {
            Console.WriteLine(item);
        }
    }

    public class RawEntity
    {
        public int id { get; init; }
    }

    public class TransformedEntity : RawEntity
    {
        public string TmdbId { get; init; }
    }
}
using DramaDayScraper.Table;
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
    public static void Main()
    {
        var htmlWeb = new HtmlWeb();
        var htmlDoc = htmlWeb.LoadFromWebAsync("https://dramaday.me/iris/").Result;
        var tbody = htmlDoc.DocumentNode
            .SelectSingleNode(".//tbody");

        var result = TableParser.Parse(tbody);

        Console.WriteLine(JsonSerializer.Serialize(result));

        Console.WriteLine();
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
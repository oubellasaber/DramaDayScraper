using DramaDayScraper.Table;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using HtmlAgilityPack;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main()
    {
        var htmlWeb = new HtmlWeb();
        var htmlDoc = htmlWeb.LoadFromWebAsync("https://dramaday.me/namib/").Result;
        var tbody = htmlDoc.DocumentNode
            .SelectSingleNode("//*[@id=\"supsystic-table-1010\"]/tbody");

        var options = new JsonSerializerOptions
        {
            Converters = { new EpisodeConverter() },
            WriteIndented = false // Optional: for pretty-printing
        };

        var result = TableParser.Parse(tbody);

        Console.WriteLine(JsonSerializer.Serialize(result, options));
    }
}

public class EpisodeConverter : JsonConverter<Episode>
{
    public override Episode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var root = document.RootElement;
            if (root.TryGetProperty("Type", out JsonElement typeElement))
            {
                var type = typeElement.GetString();
                Episode? value = type switch
                {
                    nameof(SpecialEpisode) => JsonSerializer.Deserialize<SpecialEpisode>(root.GetRawText(), options),
                    nameof(SingleEpisode) => JsonSerializer.Deserialize<SingleEpisode>(root.GetRawText(), options),
                    nameof(BatchEpisode) => JsonSerializer.Deserialize<BatchEpisode>(root.GetRawText(), options),
                    nameof(UknownEpisode) => JsonSerializer.Deserialize<UknownEpisode>(root.GetRawText(), options),
                    _ => throw new NotSupportedException($"Unknown type: {type}")
                };
                return value!;
            }
        }
        throw new JsonException("Invalid JSON for Episode");
    }

    public override void Write(Utf8JsonWriter writer, Episode value, JsonSerializerOptions options)
    {
        var type = value.GetType().Name;
        var json = JsonSerializer.Serialize(value, value.GetType(), options);
        using (JsonDocument document = JsonDocument.Parse(json))
        {
            writer.WriteStartObject();
            writer.WriteString("Type", type);
            foreach (var property in document.RootElement.EnumerateObject())
            {
                property.WriteTo(writer);
            }
            writer.WriteEndObject();
        }
    }
}

/**/
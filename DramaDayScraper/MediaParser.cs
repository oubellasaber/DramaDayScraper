using Core.Abstraction;
using DramaDayScraper.Extentions.Pipeline;
using DramaDayScraper.MediaInfo;
using DramaDayScraper.Table;
using HtmlAgilityPack;

namespace DramaDayScraper
{
    internal class MediaParser : IParser<HtmlNode, Result<Media>>
    {
        public class MediaPipelineState
        {
            public Error? Error { get; set; }
            public Media Media { get; set; }
        }

        public static Result<Media> Parse(HtmlNode input)
        {
            MediaPipelineState state = new MediaPipelineState();

            Pipeline<MediaPipelineState>
               .For(input, state)
               .Try(
                   parser: MediaInfoParsingHandler.Parse,
                   onSuccess: (media, state) => state.Media = media,
                   onFailure: (result, state) => state.Error ??= result.Error,
                   isContinue: true
               )
               .Try(
                   parser: input => TableParser.Parse(input.SelectSingleNode(".//tbody")),
                   onSuccess: (seasons, state) => state.Media.Seasons = seasons,
                   onFailure: (result, state) => state.Error ??= result.Error,
                   isContinue: true
               );

            return state.Media;
        }
    }
}

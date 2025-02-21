using DramaDayTransformer.LinkScraping;
using DramaDayTransformer.LinkScraping.Links;

namespace DramaDayTransformer.Tests
{
    public class RawHtmlParsingHandlerTests
    {
        private const string SampleHtmlContent = @"01-10:
https://mega.nz/file/pHZizDIQ#7wxESzbcgHLyup1ttbQhnVfe4ElCQmbq0u28-kgYTpw

1. https://dddrive.me/VIVVfzl
https://dddrive.me/eHIjSvn
https://dddrive.me/83KOgqA
https://dddrive.me/P0mC86k
https://dddrive.me/aCFhMP3
https://dddrive.me/QsDMpy0
https://dddrive.me/cOHMsN3
https://dddrive.me/sMAm3W8
https://dddrive.me/7aJ8RRT
https://dddrive.me/z7qEIh1
https://pixeldrain.com/u/6Lo93B3D
https://pixeldrain.com/u/P2YNa9s5
https://pixeldrain.com/u/z386DxMd
https://pixeldrain.com/u/VRhDmYnF
https://pixeldrain.com/u/GjfSYC5B
https://pixeldrain.com/u/Cihot5UP
https://pixeldrain.com/u/UoYKvYS2
https://pixeldrain.com/u/gtx8K6wY
https://pixeldrain.com/u/VfmyprPN
https://pixeldrain.com/u/v7YgD14Z";

        [Fact]
        public void ParseHtml_WithValidInput_ReturnsCorrectNumberOfLinks()
        {
            // Arrange & Act
            var result = RawHtmlParsingHandler.ParseLines(SampleHtmlContent);

            // Assert
            Assert.Equal(21, result.Count); // 12 individual episodes + 5 batch links
        }

        [Fact]
        public void ParseHtml_WithValidInput_ParsesIndividualEpisodeLinksCorrectly()
        {
            // Arrange & Act
            var result = RawHtmlParsingHandler.ParseLines(SampleHtmlContent);
            var singleLinksCount = result.Where(l => l.GetType() == typeof(SingleLink)).Count();

            // Assert
            Assert.Equal(20, singleLinksCount);
        }

        [Fact]
        public void ParseHtml_WithValidInput_ParsesBatchLinksCorrectly()
        {
            // Arrange & Act
            var result = RawHtmlParsingHandler.ParseLines(SampleHtmlContent);
            var batchLinksCount = result.Where(l => l.GetType() == typeof(BatchLink)).Count();

            // Assert
            Assert.Equal(1, batchLinksCount);
        }
    }
}
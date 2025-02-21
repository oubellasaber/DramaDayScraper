using DramaDayTransformer.LinkScraping;

namespace Test.DramaDayTransformer.DramaDayTransformer.UnitTests
{
    public class BatchLinkParserTests
    {
        [Theory]
        [InlineData("01-12: https://pixeldrain.com/l/nHMxXuqT", 1, 12, "https://pixeldrain.com/l/nHMxXuqT")]
        [InlineData("01-12 https://pixeldrain.com/l/nHMxXuqT", 1, 12, "https://pixeldrain.com/l/nHMxXuqT")]
        [InlineData("01-12https://pixeldrain.com/l/nHMxXuqT", 1, 12, "https://pixeldrain.com/l/nHMxXuqT")]
        [InlineData("01 12 https://pixeldrain.com/l/nHMxXuqT", 1, 12, "https://pixeldrain.com/l/nHMxXuqT")]
        public void Parse_ValidInput_ReturnsSuccess(string input, int expectedStart, int expectedEnd, string expectedLink)
        {
            // Act
            var result = BatchLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedStart, result.Value.RangeStart);
            Assert.Equal(expectedEnd, result.Value.RangeEnd);
            Assert.Equal(expectedLink, result.Value.DirectLink);
        }

        [Theory]
        [InlineData("01- https://pixeldrain.com/l/nHMxXuqT")] // Invalid: missing range end
        [InlineData("01-12")] // Invalid: no URL
        [InlineData("12-01 https://pixeldrain.com/l/nHMxXuqT")] // Invalid: start > end
        public void Parse_InvalidInput_ReturnsFailure(string input)
        {
            // Act
            var result = BatchLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}

using DramaDayTransformer.RawHtmlScraping;

namespace DramaDayTransformer.UnitTests
{
    public class SpecialLinkarserTests
    {
        [Theory]
        [InlineData("This is a special episode https://example.com", "This is a special episode", "https://example.com")]
        [InlineData("Special Row: The Mystery https://example.com/special-link", "Special Row: The Mystery", "https://example.com/special-link")]
        public void Parse_ValidInputWithSpecial_ReturnsSuccess(string input, string expectedTitle, string expectedLink)
        {
            // Act
            var result = SpecialLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedTitle, result.Value.Title);
            Assert.Equal(expectedLink, result.Value.DirectLink);
        }

        [Theory]
        [InlineData("01: https://example.com")]
        [InlineData("01-02: https://example.com/special-link")]
        public void Parse_TitleWithoutSpecialWord_ReturnsFailure(string input)
        {
            // Act
            var result = SpecialLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Theory]
        [InlineData("https://example.com")]
        [InlineData("Special Title without URL")]
        [InlineData("No valid URL here")]
        public void Parse_MissingLink_ReturnsFailure(string input)
        {
            // Act
            var result = SpecialLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Theory]
        [InlineData("Special Row Title")]
        [InlineData("Title without URL")]
        public void Parse_MissingTitle_ReturnsFailure(string input)
        {
            // Act
            var result = SpecialLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Theory]
        [InlineData("  Special Title   https://example.com")]
        [InlineData("special spaces   https://example.com")]
        public void Parse_InputWithExtraSpaces_ReturnsSuccess(string input)
        {
            // Act
            var result = SpecialLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }

        [Theory]
        [InlineData("Special Title here http://")]
        [InlineData("Special Title with invalid url https://")]
        public void Parse_InvalidUrlFormat_ReturnsFailure(string input)
        {
            // Act
            var result = SpecialLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}

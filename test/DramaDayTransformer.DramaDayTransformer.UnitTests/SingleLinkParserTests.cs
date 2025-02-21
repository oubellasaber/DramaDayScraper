using DramaDayTransformer.LinkScraping;

namespace Test.DramaDayTransformer.DramaDayTransformer.UnitTests
{
    public class SingleLinkParserTests
    {
        [Theory]
        [InlineData("01. https://dddrive.me/B10UglVD", "https://dddrive.me/B10UglVD")]
        [InlineData("1: https://dddrive.me/B-20UglVD", "https://dddrive.me/B-20UglVD")]
        [InlineData("01https://dddrive.me/BCUglVD", "https://dddrive.me/BCUglVD")]
        [InlineData("01 https://dddrive.me/BCUglVD", "https://dddrive.me/BCUglVD")]
        [InlineData("01:https://dddrive.me/BCUglVD", "https://dddrive.me/BCUglVD")]
        [InlineData("01 : https://dddrive.me/BCUglVD", "https://dddrive.me/BCUglVD")]
        [InlineData("https://dddrive.me/BCUglVL", "https://dddrive.me/BCUglVL")]
        [InlineData("1.https://dddrive.me/BCUglVD", "https://dddrive.me/BCUglVD")]
        public void Parse_ValidInput_ReturnsSingleLink(string input, string expectedLink)
        {
            // Act
            var result = SingleLinkParser.Parse(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedLink, result.Value.DirectLink);
        }

        [Theory]
        [InlineData("01-12: https://dddrive.me/BCUglVL")]
        [InlineData("01: 12 https://dddrive.me/BCUglVL")]
        [InlineData("01:_12: https://dddrive.me/BCUglVL")]
        [InlineData("01.")]
        public void Parse_InvalidInput_ReturnsFailure(string input)
        {
            // Act
            var result = SingleLinkParser.Parse(input);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}

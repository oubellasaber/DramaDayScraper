using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Pipeline;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.MediaVersions
{
    internal class MediaVersionParser : IParser<HtmlNode, Result<MediaVersion>>,
        IValidator<HtmlNode, Result>
    {
        public static Result<MediaVersion> Parse(HtmlNode input)
        {
            MediaVersion? mediaVersion = null;

            mediaVersion = Pipeline<MediaVersion>
                .For(input, mediaVersion)
                .Try(
                    parserValidator: HorizontalMediaVersionParser.ValidateAndParse,
                    onSuccess: (horizontalMediaVersion, state) => state = horizontalMediaVersion
                )
                .Try(
                    parserValidator: SidebarMediaVersionParser.ValidateAndParse,
                    onSuccess: (sidebarMediaVersion, state) => state = sidebarMediaVersion
                )
                .GetState();

            return ReferenceEquals(mediaVersion, null)
                ? Result.Failure<MediaVersion>(Error.NoSuitableParserFound)
                : Result.Success<MediaVersion>(mediaVersion);
        }


        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<MediaVersion> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, MediaVersion>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.MediaVersions
{
    internal class SidebarMediaVersionParser : IParser<HtmlNode, Result<MediaVersion>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<MediaVersion> Parse(HtmlNode input)
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

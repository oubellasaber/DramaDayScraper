using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup
{
    internal class ThreeCellLinksGroupParser : IParser<HtmlNode, Result<ICollection<string>>>,
        IValidator<HtmlNode, Result>
    {
        public static Result<ICollection<string>> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<ICollection<string>> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, ICollection<string>>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

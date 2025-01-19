using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Abtraction
{
    internal class HtmlNodeParserWithValidation<TResult>
        : ParserWithValidation<HtmlNode, TResult>
    {
        public HtmlNodeParserWithValidation(
            IValidator<HtmlNode, Result> validator, 
            IParser<HtmlNode, Result<TResult>> parser)
            : base(validator, parser)
        {
        }
    }
}

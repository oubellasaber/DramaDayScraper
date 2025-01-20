using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Pipeline;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup
{
    internal class LinkGroupParser : IParser<HtmlNode, Result<ICollection<string>>>,
        IValidator<HtmlNode, Result>
    {
        public static Result<ICollection<string>> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result Validate(HtmlNode input)
        {
            ICollection<string>? linksGroups = null;

            linksGroups = Pipeline<ICollection<string>?>
                .For(input, linksGroups)
                .Try(
                    parserValidator: NoTableLinksGroupParser.ValidateAndParse,
                    onSuccess: (noTableLinksGroups, state) => state = noTableLinksGroups
                )
                .Try(
                    parserValidator: ThreeCellLinksGroupParser.ValidateAndParse,
                    onSuccess: (threeCellLinksGroup, state) => state = threeCellLinksGroup
                )
                .Try(
                    parserValidator: TwoCellLinksGroupParser.ValidateAndParse,
                    onSuccess: (twoCellLinksGroup, state) => state = twoCellLinksGroup
                )
                .GetState();

            return ReferenceEquals(linksGroups, null)
                ? Result.Failure<ICollection<string>?>(Error.NoSuitableParserFound)
                : Result.Success<ICollection<string>?>(linksGroups);
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

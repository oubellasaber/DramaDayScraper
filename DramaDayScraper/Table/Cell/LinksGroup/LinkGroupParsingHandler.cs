using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Pipeline;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.LinksGroup
{
    internal class LinkGroupParsingHandler : IParser<HtmlNode, Result<ICollection<ICollection<Link>>>>
    {
        public static Result<ICollection<ICollection<Link>>> Parse(HtmlNode input)
        {
            ValueErrorState<ICollection<ICollection<Link>>> linksGroupsState = new();

            Pipeline<ValueErrorState<ICollection<ICollection<Link>>>>
                .For(input, linksGroupsState)
                .Try(
                    parserValidator: NoTableLinksGroupParser.ValidateAndParse,
                    onSuccess: (noTableLinksGroups, state) => state.Value = noTableLinksGroups,
                    onFailure: (result, state) => state.Error ??= result.Error
                )
                .Try(
                    parserValidator: ThreeCellLinksGroupParser.ValidateAndParse,
                    onSuccess: (threeCellLinksGroup, state) => state.Value = threeCellLinksGroup,
                    onFailure: (result, state) => state.Error ??= result.Error
                )
                .Try(
                    parserValidator: TwoCellLinksGroupParser.ValidateAndParse,
                    onSuccess: (twoCellLinksGroup, state) => state.Value = twoCellLinksGroup,
                    onFailure: (result, state) => state.Error ??= result.Error
                );

            return ReferenceEquals(linksGroupsState.Value, null)
                ? Result.Failure<ICollection<ICollection<Link>>>(linksGroupsState.Error!)
                : Result.Success(linksGroupsState.Value);
        }
    }
}

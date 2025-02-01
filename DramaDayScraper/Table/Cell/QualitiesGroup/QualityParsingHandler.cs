using DramaDayScraper.Abstraction;
using DramaDayScraper.Extentions.Pipeline;
using DramaDayScraper.Table.Cell.QualitiesGroup.NoTableQualitiesGroup;
using DramaDayScraper.Table.Cell.QualitiesGroup.ThreeCellQualitiesGroup;
using DramaDayScraper.Table.Cell.QualitiesGroup.TwoCellQualitiesGroup;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup
{
    internal class QualityParsingHandler : IParser<HtmlNode, Result<ICollection<string>>>
    {
        public static Result<ICollection<string>> Parse(HtmlNode input)
        {
            ValueErrorState<ICollection<string>> qualitiesGroups = new();

            Pipeline<ValueErrorState<ICollection<string>>>
               .For(input, qualitiesGroups)
               .Try(
                   parserValidator: NoTableQualitiesGroupParser.ValidateAndParse,
                   onSuccess: (noTableQualitiesGroups, state) => state.Value = noTableQualitiesGroups,
                   onFailure: (result, state) => state.Error ??= result.Error
               )
               .Try(
                   parserValidator: ThreeCellQualitiesGroupParser.ValidateAndParse,
                   onSuccess: (threeCellQualitiesGroups, state) => state.Value = threeCellQualitiesGroups,
                   onFailure: (result, state) => state.Error ??= result.Error
               )
               .Try(
                   parserValidator: TwoCellQualitiesGroupParser.ValidateAndParse,
                   onSuccess: (twoCellQualitiesGroups, state) => state.Value = twoCellQualitiesGroups,
                   onFailure: (result, state) => state.Error ??= result.Error
               )
               .GetState();

            return ReferenceEquals(qualitiesGroups.Value, null)
                ? Result.Failure<ICollection<string>>(qualitiesGroups.Error!)
                : Result.Success<ICollection<string>>(qualitiesGroups.Value);
        }
    }
}

using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.QualitiesGroup.NoTableQualitiesGroup;
using DramaDayScraper.Table.Cell.QualitiesGroup.ThreeCellQualitiesGroup;
using DramaDayScraper.Table.Cell.QualitiesGroup.TwoCellQualitiesGroup;
using DramaDayScraper.Table.Pipeline;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.QualitiesGroup
{
    internal class QualityGroupParser : IParser<HtmlNode, Result<ICollection<string>>>,
        IValidator<HtmlNode, Result>
    {
        public static Result<ICollection<string>> Parse(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result Validate(HtmlNode input)
        {
            ICollection<string>? qualitiesGroups = null;

            qualitiesGroups = Pipeline<ICollection<string>?>
                .For(input, qualitiesGroups)
                .Try(
                    parserValidator: NoTableQualitiesGroupParser.ValidateAndParse,
                    onSuccess: (noTableQualitiesGroups, state) => state = noTableQualitiesGroups
                )
                .Try(
                    parserValidator: ThreeCellQualitiesGroupParser.ValidateAndParse,
                    onSuccess: (threeCellQualitiesGroups, state) => state = threeCellQualitiesGroups
                )
                .Try(
                    parserValidator: TwoCellQualitiesGroupParser.ValidateAndParse,
                    onSuccess: (twoCellQualitiesGroups, state) => state = twoCellQualitiesGroups
                )
                .GetState();

            return ReferenceEquals(qualitiesGroups, null)
                ? Result.Failure<ICollection<string>?>(Error.NoSuitableParserFound)
                : Result.Success<ICollection<string>?>(qualitiesGroups);
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

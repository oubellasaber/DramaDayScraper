using DramaDayScraper.Abstraction;
using DramaDayScraper.Table.Cell.Abtraction;
using DramaDayScraper.Table.Cell.LinksGroup;
using DramaDayScraper.Table.Cell.QualitiesGroup;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.EpisodeVersion
{
    internal class EpisodeVersionParser : IParser<HtmlNode, Result<ICollection<EpVersion>>>,
        IValidator<HtmlNode, Result>
    {
        public static Result Validate(HtmlNode input)
        {
            throw new NotImplementedException();
        }

        public static Result<ICollection<EpVersion>> Parse(HtmlNode input)
        {
            ICollection<EpVersion>? epVersion = null;

            var qualitiesGroups = LinkGroupParser.ValidateAndParse(input);
            if (qualitiesGroups.IsFailure)
            {

            }

            var linksGroups = QualityGroupParser.ValidateAndParse(input);
            if (linksGroups.IsFailure)
            {

            }

            // merge together using utility and return

            return Result.Success();
        }

        public static Result<ICollection<EpVersion>> ValidateAndParse(HtmlNode input)
        {
            return ParserWithValidation<HtmlNode, ICollection<EpVersion>>.ParseWithValidation(
                input,
                Validate,
                Parse
            );
        }
    }
}

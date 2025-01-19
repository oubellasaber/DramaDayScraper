using DramaDayScraper.Abstraction;

namespace DramaDayScraper.Table.Cell.Abtraction
{
    internal class ParsingService<TInput, TResult>
    {
        private readonly IEnumerable<ParserWithValidation<TInput, TResult>> _parsersWithValidation;

        public ParsingService(IEnumerable<ParserWithValidation<TInput, TResult>> parsersWithValidation)
        {
            _parsersWithValidation = parsersWithValidation;
        }

        public Result<TResult> ValidateAndParse(TInput input)
        {
            var result = _parsersWithValidation
                .Select(epwv => epwv.ParseWithValidation(input))
                .FirstOrDefault();

            return Result.Failure<TResult>(Error.NoSuitableParserFound);
        }
    }
}

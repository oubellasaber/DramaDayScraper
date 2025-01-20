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
            foreach (var parser in _parsersWithValidation)
            {
                var result = parser.ParseWithValidation(input);
                if (result.IsSuccess)
                {
                    return result;
                }
            }

            return Result.Failure<TResult>(Error.NoSuitableParserFound);
        }
    }
}

using DramaDayScraper.Abstraction;

namespace DramaDayScraper.Table.Cell.Abtraction
{
    internal class ParserWithValidation<TInput, TResult>
    {
        private readonly IValidator<TInput, Result> _validator;
        private readonly IParser<TInput, Result<TResult>> _parser;

        public ParserWithValidation(IValidator<TInput, Result> validator, IParser<TInput, Result<TResult>> parser)
        {
            _validator = validator;
            _parser = parser;
        }

        public Result<TResult> ParseWithValidation(TInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsSuccess)
            {
                return Result.Failure<TResult>(validationResult.Error);
            }

            return _parser.Parse(input);
        }
    }
}

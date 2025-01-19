namespace DramaDayScraper.Abstraction
{
    internal interface IValidator<TInput, TResult>
    {
        TResult Validate(TInput input);
    }
}

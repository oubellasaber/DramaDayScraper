namespace DramaDayScraper.Abstraction
{
    internal interface IValidator<TInput, TResult>
    {
        abstract static TResult Validate(TInput input);
    }
}

namespace DramaDayScraper.Abstraction
{
    public interface IParser<TInput, TResult>
    {
        TResult Parse(TInput input);
    }
}

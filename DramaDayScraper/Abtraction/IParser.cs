using DramaDayScraper.Table.Cell.Episodes.BatchEpisode;

namespace DramaDayScraper.Abstraction
{
    public interface IParser<TInput, TResult>
    {
        abstract static TResult Parse(TInput input);
    }
}

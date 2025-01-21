using DramaDayScraper.Abstraction;

namespace DramaDayScraper.Table.Pipeline
{
    public class ValueErrorState<T>
    {
        public T? Value { get; set; }
        public Error? Error { get; set; }
    }
}

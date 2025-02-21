using Core.Abstraction;

namespace DramaDayScraper.Extentions.Pipeline
{
    public class ValueErrorState<T>
    {
        public T? Value { get; set; }
        public Error? Error { get; set; }
    }
}
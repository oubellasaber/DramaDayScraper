namespace Core.Abstraction
{
    public interface IParser<TInput, TResult>
    {
        public abstract static TResult Parse(TInput input);
    }
}

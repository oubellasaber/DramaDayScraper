namespace Core.Abstraction
{
    public interface IValidator<TInput, TResult>
    {
        abstract static TResult Validate(TInput input);
    }
}

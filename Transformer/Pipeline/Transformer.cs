using Core.Abstraction;

namespace DramaDayTransformer.Pipeline
{
    public interface ITransformer<TInput, TOutput>
        where TInput : notnull
        where TOutput : notnull
    {
        Task<Result<TOutput>> TransformAsync(TInput input);
    }
}

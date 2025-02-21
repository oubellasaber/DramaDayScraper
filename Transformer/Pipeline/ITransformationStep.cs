using Core.Abstraction;

namespace DramaDayTransformer.Pipeline.TransformationSteps
{
    public interface ITransformationStep<TInput, TOutput>
    {
        ValueTask<Result<TOutput>> TransformAsync(TInput input);
    }

    public interface ITransformationStep<TInput1, TInput2, TOutput>
    {
        ValueTask<Result<TOutput>> TransformAsync(TInput1 input1, TInput2 input2);
    }

}

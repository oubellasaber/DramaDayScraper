using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Pipeline
{
    public class Pipeline<TState>
    {
        private readonly PipelineContext<TState> _context;

        private Pipeline(PipelineContext<TState> context)
        {
            _context = context;
        }

        public static Pipeline<TState> For(HtmlNode node, TState initialState)
            => new(new PipelineContext<TState>(node, initialState));

        public Pipeline<TState> Try<T>(
            Func<HtmlNode, Result<T>> parserValidator,
            Action<T, TState> onSuccess,
            Action<Result<T>, TState>? onFailure = null,
            bool isContinue = false)
        {
            if (!_context.ContinueProcessing) return this;

            var result = parserValidator(_context.Node);
            if (result.IsSuccess)
            {
                onSuccess(result.Value, _context.State);
                _context.ContinueProcessing = isContinue;
            }
            else
            {
                onFailure?.Invoke(result, _context.State);
            }
            return this;
        }

        //public Pipeline<TState> Try<T>(
        //    Func<HtmlNode, Result<T>> parserValidator,
        //    Action<T, TState> onSuccess,
        //    bool isContinue = false)
        //{
        //    if (!_context.ContinueProcessing) return this;

        //    var result = parserValidator(_context.Node);
        //    if (result.IsSuccess)
        //    {
        //        onSuccess(result.Value, _context.State);
        //        _context.ContinueProcessing = isContinue;
        //    }
        //    return this;
        //}

        public Pipeline<TState> EnsureExists(
            Func<TState, bool> condition,
            Action<TState> action)
        {
            if (condition(_context.State))
            {
                action(_context.State);
            }
            return this;
        }

        public TState Finally(Action<PipelineContext<TState>> finalAction)
        {
            if (_context.ContinueProcessing)
            {
                finalAction(_context);
            }

            return GetState();
        }

        public TState GetState() => _context.State;
    }
}

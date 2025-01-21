using HtmlAgilityPack;

namespace DramaDayScraper.Table.Pipeline
{
    public class PipelineContext<TState>
    {
        public HtmlNode Node { get; }
        public TState State { get; }
        public bool ContinueProcessing { get; set; }

        public PipelineContext(HtmlNode node, TState state)
        {
            Node = node;
            State = state;
            ContinueProcessing = true;
        }
    }
}

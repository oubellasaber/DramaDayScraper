using HtmlAgilityPack;

namespace DramaDayScraper.Table.Pipeline
{
    internal class PipelineContext<TState>
    {
        public HtmlNode Node { get; set; }
        public TState State { get; set; }
        public bool ContinueProcessing { get; set; } = true;

        public PipelineContext(HtmlNode node, TState initialState)
        {
            Node = node;
            State = initialState;
        }
    }
}

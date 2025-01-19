using DramaDayScraper.Abstraction;
using HtmlAgilityPack;

namespace DramaDayScraper.Table.Cell.Abtraction
{
    internal interface IHtmlNodeParser<T> : IParser<HtmlNode, Result<T>>
    {
    }
}

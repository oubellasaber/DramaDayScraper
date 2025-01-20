using DramaDayScraper.Table.Cell.Episodes;
using DramaDayScraper.Table.Cell.Episodes.Entities;
using HtmlAgilityPack;

public class Program
{
    public static void Foo(Episode ep)
    {
        Console.WriteLine(ep.GetType());
    }
    public static void Main()
    {
        Foo(new BatchEpisode());
    }
}
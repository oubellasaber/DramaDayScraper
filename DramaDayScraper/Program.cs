namespace DramaDayScraper
{
    internal class Program
    {
        public abstract class A
        {
            public List<string> Links { get; set; } = new();
        }

        public class AB : A
        {
            public string Ab { get; set; } = "Hi";

            public string GetAb() => Ab;
        }

        static void Main(string[] args)
        {
            A a = new AB();
            Console.WriteLine(((AB)a).Ab);
        }
    }
}

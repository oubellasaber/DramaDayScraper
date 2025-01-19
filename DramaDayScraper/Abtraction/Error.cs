namespace DramaDayScraper.Abstraction
{
    public record Error(string Code, string Name)
    {
        public static Error None = new(string.Empty, string.Empty);
        public static Error NullValue = new("Error.NullValue", "Null value was provided");
        public static Error NoSuitableParserFound = new Error("Error.NoSuitableParserFound", "No suitable parser found for the provided input.")
    }
}

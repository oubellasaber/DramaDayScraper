namespace DramaDayScraper.Abstraction
{
    public record Error(string Code, string Name)
    {
        public static Error None = new(string.Empty, string.Empty);
        public static Error NullValue = new("Error.NullValue", "Null value was provided");
        public static Error NoSuitableParserFound = new Error("Error.NoSuitableParserFound", "No suitable parser found for the provided input.");
        public static Error HtmlNodeIsNotTr = new("Error.HtmlNodeIsNotTr", "The provided HTML node is not a <tr> element");
        public static Error RowStructureIsNotSupported = new("Error.RowStructureIsNotSupported", "<tr> element has unexpected number of <td> elements");
        public static Error MismatchedParser = new("Error.MismatchedParser", "The provided HTML node does not match the expected format for the parser.");
        public static Error NotExpectedFormat = new("Error.NotExpectedFormat", "the <td> element is not in the expected Format");


    }
}

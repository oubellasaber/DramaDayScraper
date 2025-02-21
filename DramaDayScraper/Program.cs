using DramaDayScraper.DALtest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        var client = new HttpClient();
        var response = client.GetAsync("https://filecrypt.co/Create.html").Result;

        foreach (var item in response.Headers.GetValues("Set-Cookie"))
        {
            Console.WriteLine(item.ToString());
        }
    }

    static void Mdain(string[] args)
    {
        DramaDayContext ctx = new();
        var links = ctx.ShortLinks
    .Where(sl => sl.DirectLink.Contains("pastebin") ||
                 sl.DirectLink.Contains("paste2") ||
                 sl.DirectLink.Contains("rentry"))
            .Select(sl => sl.DirectLink)
            .ToList();


        // Selenium ChromeDriver setup
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized"); // Optional: Open Chrome maximized

        using var driver = new ChromeDriver(options);

        // Process links in batches of 5
        for (int i = 0; i < links.Count; i += 5)
        {
            // Get the next batch of 5 links
            var batch = links.Skip(i).Take(5).ToList();

            Console.WriteLine($"Opening batch {i / 5 + 1}...");

            var openTabs = new List<IWebDriver>();

            // Open each link in a new tab
            foreach (var link in batch)
            {
                IWebDriver newTab = driver.SwitchTo().NewWindow(WindowType.Tab);
                if (link?.Contains("rentry") ?? false)
                    newTab.Navigate().GoToUrl(link.Replace("/raw", ""));
                else
                {
                    newTab.Navigate().GoToUrl(link);
                }

                openTabs.Add(newTab);
                Console.WriteLine($"Opened: {link}");
            }

            Console.WriteLine("Press Enter to close these tabs and move to the next batch...");
            Console.ReadLine(); // Wait for user input to proceed

            var handles = driver.WindowHandles.ToList();
            for (int j = 1; j < handles.Count; j++) // Skip the first handle (main window)
            {
                driver.SwitchTo().Window(handles[j]);
                driver.Close();
            }

            // Switch back to the main window
            driver.SwitchTo().Window(handles[0]);
        }

        Console.WriteLine("All links processed!");
    }
}

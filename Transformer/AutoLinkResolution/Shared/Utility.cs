namespace LinkTransformer.AutoLinkResolution.Shared
{
    public static class Utility
    {
        public static async Task<string?> GetFirstRedirectAsync(string url)
        {
            using HttpClientHandler handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            using HttpClient client = new HttpClient(handler);

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if ((int)response.StatusCode >= 300 && (int)response.StatusCode < 400)
                {

                    if (response.Headers.Location != null)
                    {
                        return response.Headers.Location.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }
    }
}

namespace DramaDayTransformer.Link.LinkCollection.FileCrypt
{
    public class EpisodeLink
    {
        public string Title { get; set; }
        public string BaseLink { get; set; }
        public string DataAttributeForLink { get; set; }
        public string DirectLink { get; set; }
        
        public EpisodeLink(string title, string baseLink, string dataAttributeForLink)
        {
            Title = title;
            BaseLink = baseLink;
            DataAttributeForLink = dataAttributeForLink;
        }
    }
}

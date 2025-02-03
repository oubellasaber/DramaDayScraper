namespace DramaDayTransformer.Link.AutoLinkResolution
{
    public interface IResolver
    {
        Task<string?> ResolveLink(string link);
    }
}

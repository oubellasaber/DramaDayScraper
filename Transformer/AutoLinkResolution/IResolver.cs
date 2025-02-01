namespace LinkTransformer.AutoLinkResolution
{
    public interface IResolver
    {
        Task<string?> ResolveLink(string link);
    }
}

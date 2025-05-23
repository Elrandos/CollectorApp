namespace inapp.Interfaces.Providers
{
    public interface IGuidProvider
    {
        Guid GenerateGuid();
        string GenerateGuidAsString();
        string GenerateToken();
        Guid GuidFromInt(int id);
    }
}
using inapp.Interfaces.Providers;

namespace inapp.Providers
{
    public class GuidProvider : IGuidProvider
    {
        public Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        public string GenerateGuidAsString()
        {
            return Guid.NewGuid().ToString();
        }

        public string GenerateToken()
        {
            return Guid.NewGuid().ToString("N");
        }

        public Guid GuidFromInt(int id)
        {
            return new Guid(id, 0, 0, new byte[8]);
        }
    }
}

using inapp.Interfaces.Services;
using inapp.Models;

namespace inapp.Services
{
    public class CollectionService : ICollectionService
    {
        public CollectionService() { }

        public async Task<List<CollectionItem>> GetAllForCurrentUser()
        {
            var result = new List<CollectionItem>();
            result.Add(new CollectionItem
            {
                CollectionId = new Guid(),
                Collection = new UserCollection(),
                Description = "TAKI CHUJ"
            });
            return result;
        }
    }
}

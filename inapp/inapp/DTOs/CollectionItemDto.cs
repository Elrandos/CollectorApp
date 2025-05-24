using inapp.Models;

namespace inapp.DTOs
{
    public class CollectionItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}

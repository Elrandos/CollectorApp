namespace inapp.Models
{
    public class UserCollection
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<CollectionItem> Items { get; set; } = new();
    }
}

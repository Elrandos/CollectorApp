namespace inapp.Models
{
    public class CollectionItem
    {
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public UserCollection Collection { get; set; }
    }
}

namespace inapp.Models
{
    public class UserCollection
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<CollectionItem> CollectionItems { get; set; }
    }
}

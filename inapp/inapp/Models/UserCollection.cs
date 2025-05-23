using System.ComponentModel.DataAnnotations;

namespace inapp.Models
{
    public class UserCollection
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? ImageUrl { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<CollectionItem> CollectionItems { get; set; }
    }
}

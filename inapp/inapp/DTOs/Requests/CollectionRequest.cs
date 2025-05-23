using System.ComponentModel.DataAnnotations;

namespace inapp.DTOs.Requests
{
    public class CollectionRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
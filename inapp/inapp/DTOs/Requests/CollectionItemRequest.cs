using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace inapp.DTOs.Requests
{
    public class CollectionItemRequest
    {
        public Guid CollectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
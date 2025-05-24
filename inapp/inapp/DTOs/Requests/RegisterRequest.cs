using System.ComponentModel.DataAnnotations;
using inapp.Attribiutes;

namespace inapp.DTOs.Requests
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; }

        [Required]
        [ValidationEmail]
        public string Email { get; set; }

        [Required]
        [Password]
        public string Password { get; set; }
    }
}

using inapp.Enums;

namespace inapp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set;  }

        public virtual ICollection<UserCollection> UserCollections { get; set; }
    }
}

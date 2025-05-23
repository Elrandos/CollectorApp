﻿using inapp.Enums;

namespace inapp.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public Role Role { get; set;  }
    }
}

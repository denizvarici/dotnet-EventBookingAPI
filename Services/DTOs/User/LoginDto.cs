using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs.User
{
    public class LoginDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

    }
}

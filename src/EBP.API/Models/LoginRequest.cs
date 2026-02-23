using System.ComponentModel;

namespace EBP.API.Models
{
    public class LoginRequest
    {
        [DefaultValue("some_user@domain.com")]
        public string Email { get; set; } = null!;
        [DefaultValue("SomePassword123!@#")]
        public string Password { get; set; } = null!;
    }
}

using System.ComponentModel;

namespace EBP.API.Models
{ 
    public class RegisterRequest
    {
        [DefaultValue("some_user@domain.com")]
        public string Email { get; set; } = null!;
        [DefaultValue("SomePassword123!@#")]
        public string Password { get; set; } = null!;
        [DefaultValue(true)]
        public bool IsAdmin { get; set; }
    }
}

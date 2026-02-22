namespace EBP.Domain.Entities
{
    public class ApplicationUser
    {
        public string UserId { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        private ApplicationUser() { }

        public static ApplicationUser Create(string userId, string email)
        {
            return new ApplicationUser
            {
                UserId = userId,
                Email = email
            };
        }
    }
}

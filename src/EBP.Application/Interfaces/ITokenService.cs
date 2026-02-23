namespace EBP.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateJwt(string userId, string email, IList<string> roles);
    }
}

using EBP.Domain.Exceptions;

namespace EBP.Application.UseCases
{
    public class LoginFailedException : DomainExceptionBase
    {
        public LoginFailedException(string errors) : base($"User login failed: {errors}")
        {
        }
    }
}
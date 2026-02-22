using EBP.Domain.Exceptions;

namespace EBP.Application.UseCases
{
    public class RegistrationFailedException : DomainExceptionBase
    {
        public RegistrationFailedException(string errors) : base($"User registration failed: {errors}")
        {
        }
    }
}
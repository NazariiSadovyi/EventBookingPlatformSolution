using EBP.Application.Commands;
using EBP.Application.Interfaces;
using MediatR;

namespace EBP.Application.UseCases
{
    public class RegisterUserUseCase(IIdentityService _identityService) :
        IRequestHandler<RegisterUserCommand>
    {
        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var (succeeded, errors) = await _identityService.CreateUserAsync(request.Email, request.Password);
            if (!succeeded)
            {
                var errorMessage = string.Join(", ", errors);
                throw new RegistrationFailedException(errorMessage);
            }
        }
    }
}

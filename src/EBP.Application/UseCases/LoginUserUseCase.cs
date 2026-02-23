using EBP.Application.Commands;
using EBP.Application.Interfaces;
using MediatR;

namespace EBP.Application.UseCases
{
    public class LoginUserUseCase(
        IIdentityService _identityService,
        ITokenService _tokenService)
        : IRequestHandler<LoginUserCommand, string>
    {
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var (found, userId, storedEmail, roles) = await _identityService.FindByEmailAsync(request.Email);
            if (!found || userId is null || storedEmail is null)
                throw new LoginFailedException("Invalid credentials");

            var valid = await _identityService.CheckPasswordAsync(userId, request.Password);
            if (!valid)
                throw new LoginFailedException("Invalid credentials");

            var token = _tokenService.CreateJwt(userId, storedEmail, roles);
            return token;
        }
    }
}

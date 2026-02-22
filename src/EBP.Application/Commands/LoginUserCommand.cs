using MediatR;

namespace EBP.Application.Commands
{
    public record class LoginUserCommand(
        string Email,
        string Password)
        : IRequest<string>
    {
    }
}

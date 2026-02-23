using MediatR;

namespace EBP.Application.Commands
{
    public record class RegisterUserCommand(
        string Email,
        string Password,
        bool IsAdmin)
        : IRequest
    {
    }
}

using EBP.Application.Commands;
using FluentValidation;

namespace EBP.Application.Validators
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand> 
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.StartAt)
                .NotEmpty();
            RuleFor(x => x.Duration)
                .NotEmpty();
            RuleFor(x => x.StandartTicketsCount)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.VipTicketsCount)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.StudentTicketsCount)
                .GreaterThanOrEqualTo(0);
        }
    }
}

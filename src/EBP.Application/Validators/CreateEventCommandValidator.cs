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
            RuleFor(x => x.TicketDetails)
                .NotEmpty()
                .Must(x => x.All(td => td.Count > 0))
                .WithMessage("Each ticket detail must have a count greater than 0.");
        }
    }
}

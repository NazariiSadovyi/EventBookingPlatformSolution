using EBP.Application.Commands;
using FluentValidation;

namespace EBP.Application.Validators
{
    public class CreateBookingEventCommandValidator : AbstractValidator<CreateBookingEventCommand> 
    {
        public CreateBookingEventCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.StartAt)
                .NotEmpty();
            RuleFor(x => x.Duration)
                .NotEmpty();
        }
    }
}

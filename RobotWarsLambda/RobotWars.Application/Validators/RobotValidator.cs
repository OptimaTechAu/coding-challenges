using FluentValidation;
using RobotWars.Domain;

namespace RobotWars.Application.Validators
{
    public class RobotValidator : AbstractValidator<Robot>
    {
        public RobotValidator()
        {
            RuleFor(r => r.X).GreaterThanOrEqualTo(0).WithMessage("X coordinate must be non-negative.");
            RuleFor(r => r.Y).GreaterThanOrEqualTo(0).WithMessage("Y coordinate must be non-negative.");
        }
    }
}

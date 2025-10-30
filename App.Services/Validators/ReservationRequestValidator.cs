using App.Core.Entities;
using FluentValidation;

namespace App.Services.Validators
{
    public class ReservationRequestValidator : AbstractValidator<ReservationRequest>
    {
        public ReservationRequestValidator()
        {
            RuleFor(x => x.Train)
                 .NotNull().WithMessage("Train cannot be null.");

            RuleFor(x => x.Train.Wagons)
                .NotEmpty().WithMessage("Train must have at least one wagon.");

            RuleFor(x => x.NumberOfPersonsToReserve)
                .GreaterThan(0).WithMessage("Number of passengers must be greater than zero.");

            RuleForEach(x => x.Train.Wagons).ChildRules(w =>
            {
                w.RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Wagon capacity must be greater than zero.");
                w.RuleFor(x => x.OccupiedSeats).GreaterThanOrEqualTo(0).WithMessage("Occupied seats cannot be negative.");
            });
        }
    }
}

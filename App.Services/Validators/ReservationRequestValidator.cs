using App.Core.Entities;
using FluentValidation;

namespace App.Services.Validators
{
    public class ReservationRequestValidator : AbstractValidator<ReservationRequest>
    {
        public ReservationRequestValidator()
        {
            RuleFor(x => x.Train)
                .NotNull().WithMessage("Tren bilgisi boş olamaz.");

            RuleFor(x => x.Train.Wagons)
                .NotEmpty().WithMessage("Tren en az bir vagona sahip olmalıdır.");

            RuleFor(x => x.NumberOfPersonsToReserve)
                .GreaterThan(0).WithMessage("Rezervasyon yapılacak kişi sayısı sıfırdan büyük olmalıdır.");

            RuleForEach(x => x.Train.Wagons).ChildRules(w =>
            {
                w.RuleFor(x => x.Capacity)
                    .GreaterThan(0).WithMessage("Vagon kapasitesi sıfırdan büyük olmalıdır.");

                w.RuleFor(x => x.OccupiedSeats)
                    .GreaterThanOrEqualTo(0).WithMessage("Dolu koltuk sayısı negatif olamaz.");
            });
        }
    }
}

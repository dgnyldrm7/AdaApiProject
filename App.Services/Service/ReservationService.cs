using App.Core.Entities;
using App.Core.Results;

namespace App.Services.Service
{
    public class ReservationService
    {
        public Result<ReservationResponse> Calculate(ReservationRequest request)
        {
            var result = new ReservationResponse();

            //1. Uygun vagonları bul (%70 kuralı)
            var availableWagons = request.Train.Wagons
                .Where(w => w.OccupiedSeats < w.Capacity * 0.7)
                .ToList();

            if (!availableWagons.Any())
                return Result<ReservationResponse>.Fail(
                    422,
                    "Reservation Not Possible",
                    "No available wagons under 70% occupancy rule."
                );

            //2. Tek vagonda mı olacak?
            if (!request.CanPeopleCanBePlacedInDifferentWagons)
            {
                var wagon = availableWagons.FirstOrDefault(w =>
                    (w.Capacity * 0.7) - w.OccupiedSeats >= request.NumberOfPersonsToReserve);

                if (wagon is null)
                    return Result<ReservationResponse>.Fail(
                        422,
                        "Reservation Not Possible",
                        "Cannot fit all passengers in the same wagon under 70% rule."
                    );

                result.IsReservationSuccessful = true;
                result.LocalDetail.Add(new LocalDetail
                {
                    WagonName = wagon.Name,
                    NumberOfReservedSeats = request.NumberOfPersonsToReserve
                });

                return Result<ReservationResponse>.Success(result, "Reservation successfully created.");
            }

            //3. Dağıtılabilir rezervasyon (multi-wagon)
            int remainingPassengers = request.NumberOfPersonsToReserve;

            foreach (var wagon in availableWagons)
            {
                int availableSeats = (int)(wagon.Capacity * 0.7) - wagon.OccupiedSeats;
                if (availableSeats <= 0) continue;

                int assignedSeats = Math.Min(availableSeats, remainingPassengers);
                result.LocalDetail.Add(new LocalDetail
                {
                    WagonName = wagon.Name,
                    NumberOfReservedSeats = assignedSeats
                });

                remainingPassengers -= assignedSeats;
                if (remainingPassengers == 0) break;
            }

            result.IsReservationSuccessful = remainingPassengers == 0;

            if (!result.IsReservationSuccessful)
                return Result<ReservationResponse>.Fail(
                    422,
                    "Reservation Not Possible",
                    "Not enough available seats under 70% rule."
                );

            return Result<ReservationResponse>
                .Success(result, "Reservation successfully distributed across wagons.");
        }
    }
}

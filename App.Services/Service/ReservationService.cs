using App.Core.Entities;
using App.Core.Results;

namespace App.Services.Service
{
    public class ReservationService
    {
        public Result<ReservationResponse> Calculate(ReservationRequest request)
        {
            var result = new ReservationResponse();

            //Uygun vagonları bul (%70 kuralı)
            var availableWagons = request.Train.Wagons
                .Where(w => w.OccupiedSeats < w.Capacity * 0.7)
                .ToList();

            // Eğer hiçbir vagon %70 doluluk kuralına göre uygun değilse
            if (!availableWagons.Any())
            {
                result.IsReservationSuccessful = false;
                result.LocalDetail = new List<LocalDetail>();

                return Result<ReservationResponse>.Success(
                    result,
                    "Hiçbir vagon %70 doluluk kuralına göre uygun değil."
                );
            }

            // Tek vagonda mı yapılacak?
            if (!request.CanPeopleCanBePlacedInDifferentWagons)
            {
                var wagon = availableWagons.FirstOrDefault(w =>
                    (w.Capacity * 0.7) - w.OccupiedSeats >= request.NumberOfPersonsToReserve);

                // Tüm yolcular tek vagonda sığmıyorsa
                if (wagon is null)
                {
                    result.IsReservationSuccessful = false;
                    result.LocalDetail = new List<LocalDetail>();

                    return Result<ReservationResponse>.Success(
                        result,
                        "Tüm yolcular aynı vagonda %70 doluluk kuralına göre yerleştirilemez."
                    );
                }

                // Tek vagonda başarıyla yerleştirildiyse
                result.IsReservationSuccessful = true;
                result.LocalDetail.Add(new LocalDetail
                {
                    WagonName = wagon.Name,
                    NumberOfReservedSeats = request.NumberOfPersonsToReserve
                });

                return Result<ReservationResponse>.Success(result, "Rezervasyon başarıyla oluşturuldu.");
            }

            //Birden fazla vagona dağıtılabilir rezervasyon
            int remainingPassengers = request.NumberOfPersonsToReserve;

            foreach (var wagon in availableWagons)
            {
                int availableSeats = (int)(wagon.Capacity * 0.7) - wagon.OccupiedSeats;
                if (availableSeats <= 0)
                    continue;

                int assignedSeats = Math.Min(availableSeats, remainingPassengers);

                result.LocalDetail.Add(new LocalDetail
                {
                    WagonName = wagon.Name,
                    NumberOfReservedSeats = assignedSeats
                });

                remainingPassengers -= assignedSeats;

                if (remainingPassengers == 0)
                    break;
            }

            result.IsReservationSuccessful = remainingPassengers == 0;

            //Eğer yerleştirilemediyse ama hata değil, business rule
            if (!result.IsReservationSuccessful)
            {
                result.LocalDetail = new List<LocalDetail>();

                return Result<ReservationResponse>.Success(
                    result,
                    "Toplam uygun koltuk sayısı %70 doluluk kuralına göre yetersiz."
                );
            }

            //Başarılı rezervasyon
            return Result<ReservationResponse>.Success(result, "Rezervasyon uygun vagonlara başarıyla dağıtıldı.");
        }
    }
}

namespace App.Core.Entities
{
    public class ReservationResponse
    {
        public bool IsReservationSuccessful { get; set; }
        public List<LocalDetail> LocalDetail { get; set; } = new();
    }

    public class LocalDetail
    {
        public string WagonName { get; set; } = default!;
        public int NumberOfReservedSeats { get; set; }
    }
}

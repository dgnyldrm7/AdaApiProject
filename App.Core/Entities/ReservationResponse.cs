namespace App.Core.Entities
{
    public class ReservationResponse
    {
        public bool IsReservationSuccessful { get; set; }
        public List<LocalDetail> LocalDetail { get; set; } = new();
    }

}

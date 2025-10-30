namespace App.Core.Entities
{
    public class ReservationRequest
    {
        public Train Train { get; set; } = new();
        public int NumberOfPersonsToReserve { get; set; }
        public bool CanPeopleCanBePlacedInDifferentWagons { get; set; }
    }
}
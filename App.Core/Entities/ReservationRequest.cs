using System.Text.Json.Serialization;

namespace App.Core.Entities
{
    public class ReservationRequest
    {
        [JsonPropertyName("Tren")]
        public Train Train { get; set; } = new();

        [JsonPropertyName("RezervasyonYapilacakKisiSayisi")]
        public int NumberOfPersonsToReserve { get; set; }

        [JsonPropertyName("KisilerFarkliVagonlaraYerlestirilebilir")]
        public bool CanPeopleCanBePlacedInDifferentWagons { get; set; }
    }
}
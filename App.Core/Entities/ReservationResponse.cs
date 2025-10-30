using System.Text.Json.Serialization;

namespace App.Core.Entities
{
    public class ReservationResponse
    {
        [JsonPropertyName("RezervasyonYapilabilir")]
        public bool IsReservationSuccessful { get; set; }

        [JsonPropertyName("YerlesimAyrinti")]
        public List<LocalDetail> LocalDetail { get; set; } = new();
    }

}

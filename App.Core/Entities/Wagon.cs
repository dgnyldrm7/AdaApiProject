using System.Text.Json.Serialization;

namespace App.Core.Entities
{
    public class Wagon
    {
        [JsonPropertyName("Ad")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("Kapasite")]
        public int Capacity { get; set; }

        [JsonPropertyName("DoluKoltukAdet")]
        public int OccupiedSeats { get; set; }
    }
}

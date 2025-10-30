using System.Text.Json.Serialization;

namespace App.Core.Entities
{
    public class Train
    {
        [JsonPropertyName("Ad")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("Vagonlar")]
        public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
    }
}

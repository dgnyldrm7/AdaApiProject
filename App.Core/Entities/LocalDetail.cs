using System.Text.Json.Serialization;

namespace App.Core.Entities
{
    public class LocalDetail
    {
        [JsonPropertyName("VagonAdi")]
        public string WagonName { get; set; } = default!;

        [JsonPropertyName("KisiSayisi")]
        public int NumberOfReservedSeats { get; set; }
    }
}

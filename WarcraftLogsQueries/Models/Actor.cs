using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Actor
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("gameID")]
        public int GameID { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("server")]
        public string? Server { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("subType")]
        public string? SubType { get; set; }
    }
}

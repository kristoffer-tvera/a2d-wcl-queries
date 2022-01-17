using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class Owner
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

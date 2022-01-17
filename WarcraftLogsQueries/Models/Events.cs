using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class Events
    {
        [JsonPropertyName("data")]
        public List<Event>? Data { get; set; }
    }
}

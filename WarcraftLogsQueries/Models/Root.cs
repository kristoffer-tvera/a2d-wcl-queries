using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class Root
    {
        [JsonPropertyName("data")]
        public Data? Data { get; set; }
    }


}

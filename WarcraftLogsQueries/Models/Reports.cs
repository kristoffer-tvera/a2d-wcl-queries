using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class Reports
    {
        [JsonPropertyName("current_page")]
        public int Page { get; set; }

        [JsonPropertyName("last_page")]
        public int LastPage { get; set; }

        [JsonPropertyName("data")]
        public List<Report>? Data { get; set; }
    }

}

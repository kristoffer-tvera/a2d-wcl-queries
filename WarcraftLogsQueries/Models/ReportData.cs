using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class ReportData
    {
        [JsonPropertyName("report")]
        public Report? Report { get; set; }

        [JsonPropertyName("reports")]
        public Reports? Reports { get; set; }
    }

}

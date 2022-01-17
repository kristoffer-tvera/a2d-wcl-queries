using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class Data
    {
        [JsonPropertyName("reportData")]
        public ReportData? ReportData { get; set; }
    }


}

using System.Text.Json.Serialization;

namespace WarcraftLogsQueries.Models
{
    public class MasterData
    {
        [JsonPropertyName("actors")]
        public List<Actor>? Actors { get; set; }
    }


}

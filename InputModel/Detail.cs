using Newtonsoft.Json;

namespace InputModel
{
    [JsonObject]
    public class Detail
    {
        [JsonProperty(PropertyName = "build-status")]
        public string BuildStatus { get; set; }

        [JsonProperty(PropertyName = "project-name")]
        public string ProjectName { get; set; }

        [JsonProperty(PropertyName = "build-id")]
        public string BuildId { get; set; }

        [JsonProperty(PropertyName = "current-phase")]
        public string CurrentPhase { get; set; }

        [JsonProperty()]
        public int Version { get; set; }
    }
}

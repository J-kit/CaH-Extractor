using Newtonsoft.Json;

namespace CaH.Shared.Poco
{
    public class DeckCost
    {
        [JsonProperty("unit")]
        public double Unit { get; set; }

        [JsonProperty("print_call_count")]
        public long PrintCallCount { get; set; }

        [JsonProperty("print_response_count")]
        public long PrintResponseCount { get; set; }

        [JsonProperty("exclusions")]
        public Exclusions Exclusions { get; set; }
    }

    public class Exclusions
    {
        [JsonProperty("calls")]
        public CallCard[] Calls { get; set; }

        [JsonProperty("responses")]
        public ResponseCard[] Responses { get; set; }
    }
}
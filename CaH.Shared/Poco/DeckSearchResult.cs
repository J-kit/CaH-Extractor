using Newtonsoft.Json;

namespace CaH.Shared.Poco
{
    public class DeckSearchResult
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("results")]
        public Results Results { get; set; }
    }

    public class Results
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("data")]
        public DeckSearchInformation[] Data { get; set; }
    }

    public class DeckSearchInformation : SimpleDeckInformation
    {
        [JsonProperty("sample_calls")]
        public CallCard[] SampleCalls { get; set; }

        [JsonProperty("sample_responses")]
        public ResponseCard[] SampleResponses { get; set; }
    }

    public class Author
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
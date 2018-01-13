using System;
using Newtonsoft.Json;

namespace CaH.Shared.Poco
{
    public class SimpleDeckInformation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("external_copyright")]
        public bool ExternalCopyright { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("call_count")]
        public string CallCount { get; set; }

        [JsonProperty("response_count")]
        public string ResponseCount { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }
    }

    public class DeckInformation : SimpleDeckInformation
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("unlisted")]
        public bool Unlisted { get; set; }

        [JsonProperty("copyright_holder_url")]
        public object CopyrightHolderUrl { get; set; }
    }
}
using System;
using Newtonsoft.Json;

namespace CaH.Shared.Poco
{
    public class CardInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string[] Text { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; set; }

        public bool Picked { get; set; }

        public OverrideCardType CardOverride { get; set; }
    }

    public enum OverrideCardType
    {
        Default,
        CallCard,
        Object,
        Action,
    }

    public class CallCard : CardInfo
    {
    }

    public class ResponseCard : CardInfo
    {
    }
}
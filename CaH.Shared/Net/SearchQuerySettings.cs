using System;
using CaH.Shared.Cardcast;

namespace CaH.Shared.Net
{
    public class SearchQuery
    {
        internal string Link => GetQueryUrl();

        public string Search { get; set; }
        public string Category { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 12;
        public bool Nsfw { get; set; } = true;

        public string Sort { get; set; } = "rating";
        public string Direction { get; set; } = "desc";

        public SearchQuery()
        {
        }

        public SearchQuery(string term, bool nsfw = false)
        {
            Search = term;
            Nsfw = nsfw;
        }

        internal string GetQueryUrl()//string search, string category, int offset, int limit, bool nsfw, string sort, string direction = "desc"
        {
            var limit = Math.Min(50, Limit);
            limit = Math.Max(1, Limit);

            return $"{CcGameApi.DeckUri}?category={Category}&direction={Direction}" +
                   $"&limit={limit}&nsfw={Nsfw.ToString().ToLower()}" +
                   $"&offset={Offset}&search={Search}&sort={Sort}";
        }
    }
}
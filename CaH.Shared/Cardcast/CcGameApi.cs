using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaH.Shared.Net;
using CaH.Shared.Poco;

namespace CaH.Shared.Cardcast
{
    public class CcGameApi
    {
        /*
         *  https://api.cardcastgame.com/v1/decks?category=&direction=desc&limit=12&nsfw=true&offset=0&search=German&sort=rating
         *  https://api.cardcastgame.com/v1/decks?category=&direction=desc&limit=12&nsfw=true&offset=12&search=German&sort=rating
         *
         *
         *  CARD REQUESTS
         *  https://api.cardcastgame.com/v1/decks/7431J
         *  https://api.cardcastgame.com/v1/decks/7431J/calls
         *  https://api.cardcastgame.com/v1/decks/7431J/responses
         *  https://api.cardcastgame.com/v1/decks/7431J/cost
         */

        private const string BaseUri = "https://api.cardcastgame.com/";
        public const string DeckUri = BaseUri + "v1/decks";

        public async Task<DeckContainer> DownloadDeckContainerAsync(string id = "7431J")
        {
            var deckInfoTask = DownloadCardInfoAsync<DeckInformation>(id, CcGameCalls.Default);

            var callCardsTask = DownloadCardInfoAsync<List<CallCard>>(id, CcGameCalls.Calls);
            var responseCardsTask = DownloadCardInfoAsync<List<ResponseCard>>(id, CcGameCalls.Responses);
            var deckCostTask = DownloadCardInfoAsync<DeckCost>(id, CcGameCalls.Cost);

            await Task.WhenAll(deckInfoTask, callCardsTask, responseCardsTask, deckCostTask);

            return new DeckContainer
            {
                DeckInformation = deckInfoTask.Result,
                CallCards = callCardsTask.Result,
                ResponseCards = responseCardsTask.Result,
                DeckCost = deckCostTask.Result,
            };
        }

        private async Task<T> DownloadCardInfoAsync<T>(string id, CcGameCalls call) where T : class
        {
            var url = GetDeckUri(id, call);
            return await DownloadJsonAsync<T>(url);
        }

        private string GetDeckUri(string id, CcGameCalls call)
        {
            string subcall = "";
            switch (call)
            {
                case CcGameCalls.Default:
                    subcall = "";
                    break;

                case CcGameCalls.Calls:
                case CcGameCalls.Responses:
                case CcGameCalls.Cost:
                    subcall = $"/{call.ToString().ToLower()}";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(call), call, null);
            }
            return $"{DeckUri}/{id}{subcall}";
        }

        public async Task<DeckSearchResult> SearchDeckAsync(SearchQuery query)
        {
            return await DownloadJsonAsync<DeckSearchResult>(query.Link);
        }

        public AsyncCardEnumerator ExecuteQuery(SearchQuery query)
        {
            return new AsyncCardEnumerator(this, query);
        }

        private async Task<T> DownloadJsonAsync<T>(string url) where T : class
        {
            using (var wc = WebClientFactory.GetClient())
            {
                return await wc.DownloadJsonAsync<T>(url);
            }
        }
    }

    public enum CcGameCalls
    {
        Default,
        Calls,
        Responses,
        Cost
    }
}
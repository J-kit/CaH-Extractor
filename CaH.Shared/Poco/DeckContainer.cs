using System.Collections.Generic;

namespace CaH.Shared.Poco
{
    public class DeckContainer
    {
        public DeckInformation DeckInformation { get; set; }
        public List<CallCard> CallCards { get; set; }
        public List<ResponseCard> ResponseCards { get; set; }
        public DeckCost DeckCost { get; set; }
    }
}
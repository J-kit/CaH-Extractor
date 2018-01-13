using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CaH.Shared.Extensions;
using CaH.Shared.Poco;

namespace CaH.Shared.Kgf
{
    public class Reformatter
    {
        public bool PicksOnly { get; set; }
        public bool NoNsfw { get; set; }

        private List<DeckContainer> _decks;

        public Reformatter(IEnumerable<DeckContainer> containers = null)
        {
            _decks = containers != null ? new List<DeckContainer>(containers) : new List<DeckContainer>();
        }

        public void AddDeck(DeckContainer deck)
        {
            _decks.Add(deck);
        }

        public void AddDecks(IEnumerable<DeckContainer> decks)
        {
            _decks.AddRange(decks);
        }

        public void ToFile(string fileName)
        {
            using (var of = File.Create(fileName))
            using (var fs = new StreamWriter(of))
            {
                var allCards = GetCards();

                var statements = allCards.Where(x => x is CallCard || x.CardOverride == OverrideCardType.CallCard);

                var objs = allCards.Where(x => x.CardOverride == OverrideCardType.Object || (x is ResponseCard && x.CardOverride == OverrideCardType.Default));
                var acts = allCards.Where(x => x.CardOverride == OverrideCardType.Action);

                statements.ForEach(x => fs.WriteLine(string.Join("_", x.Text) + "	STATEMENT"));
                objs.ForEach(x => fs.WriteLine(string.Join("_", x.Text) + "	OBJECT"));
                acts.ForEach(x => fs.WriteLine(string.Join("_", x.Text) + "	VERB"));
            }
        }

        private List<CardInfo> GetCards()
        {
            var allCards = _decks.SelectMany(m => m.CallCards).Cast<CardInfo>().Concat(_decks.SelectMany(m => m.ResponseCards));

            if (PicksOnly)
                allCards = allCards.Where(m => m.Picked);

            if (NoNsfw)
                allCards = allCards.Where(m => m.Nsfw == false);

            return allCards.ToList();
        }
    }
}
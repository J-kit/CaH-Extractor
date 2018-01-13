using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CaH.Shared.Cardcast;
using CaH.Shared.Extensions;
using CaH.Shared.Net;
using CaH.Shared.Poco;
using Microsoft.VisualStudio.Workspace.Async;
using Newtonsoft.Json;

namespace CaH_Extractor
{
    internal class Program
    {
        private const string SearchHistName = "SearchHist.json";
        private const string DeckDbName = "DeckDb.json";

        private static void Main(string[] args)
        {
            Logic();

            Console.ReadLine();
        }

        private static async Task RipDatabase(string searchString)
        {
            Directory.CreateDirectory(searchString);

            var i = 0;
            var cardApi = new CcGameApi();

            var deckList = new List<DeckSearchInformation>();
            var deckDb = new List<DeckContainer>();

            var acr = cardApi.ExecuteQuery(new SearchQuery(searchString, true));
            await acr.ForEachAsync(async x =>
               {
                   Console.WriteLine($"{i}/{acr.Count}: Downloading {x.Code}: '{x.Name}' by {x.Author.Username}");
                   var deck = await cardApi.DownloadDeckContainerAsync(x.Code);

                   deckList.Add(x);
                   deckDb.Add(deck);

                   if (++i % 7 == 0)
                   {
                       File.WriteAllText(searchString + "\\" + SearchHistName, JsonConvert.SerializeObject(deckList));
                       File.WriteAllText(searchString + "\\" + DeckDbName, JsonConvert.SerializeObject(deckDb));

                       Console.WriteLine("Saved database");
                   }
               });

            File.WriteAllText(searchString + "\\" + SearchHistName, JsonConvert.SerializeObject(deckList));
            File.WriteAllText(searchString + "\\" + DeckDbName, JsonConvert.SerializeObject(deckDb));

            Console.WriteLine("Saved database");
        }

        private static async void Logic()
        {
            //   await RipDatabase("German");
            // Console.WriteLine("Ripped database for 'German'");

            var rawdb = File.ReadAllText(DeckDbName);
            var db = JsonConvert.DeserializeObject<List<DeckContainer>>(rawdb);

            var callCards = db.Sum(m => m.CallCards.Count);
            var responseCards = db.Sum(m => m.ResponseCards.Count);

            Directory.CreateDirectory("CaF");

            // SaveAllDeck(db);

            var cc = 1;
            foreach (var deck in db)
            {
                SaveDeck(deck, $"{++cc} - {deck.DeckInformation.Name}");
            }

            Debugger.Break();
        }

        private static void SaveAllDeck(List<DeckContainer> db)
        {
            var exclude = new string[] { "", " + ", " = ", "." };

            using (var of = File.Create("CapiDb-nsfw.tsv"))
            using (var fs = new StreamWriter(of))
            {
                foreach (var va in db.SelectMany(m => m.CallCards))
                {
                    if (va.Text.SequenceEqual(exclude))
                        continue;

                    fs.WriteLine(String.Join("_", va.Text) + "	STATEMENT");
                }

                foreach (var va in db.SelectMany(m => m.ResponseCards))
                {
                    fs.WriteLine(String.Join("_", va.Text) + "	OBJECT");
                }
            }
        }

        private static void SaveDeck(DeckContainer container, string fileName)
        {
            var exclude = new string[] { "", " + ", " = ", "." };
            Directory.CreateDirectory("CaF-NSFW");
            using (var of = File.Create("CaF-NSFW\\" + fileName.Replace("\\", "-").Replace("/", "-").Replace("|", "-")
                                            .Replace(":", " ").Replace("?", " ").Replace("!", " ") + ".tsv"))

            using (var fs = new StreamWriter(of))
            {
                foreach (var va in container.CallCards.Where(m => m.Nsfw && !m.Text.SequenceEqual(exclude)))
                {
                    fs.WriteLine(String.Join("_", va.Text) + "	STATEMENT");
                }

                foreach (var va in container.ResponseCards.Where(m => m.Nsfw && !m.Text.SequenceEqual(exclude)))
                {
                    fs.WriteLine(String.Join("_", va.Text) + "	OBJECT");
                }
            }
        }
    }
}
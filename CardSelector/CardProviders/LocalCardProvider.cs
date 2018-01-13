using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaH.Shared.Poco;
using Newtonsoft.Json;

namespace CardSelector.CardProviders
{
    internal class LocalCardProvider
    {
        private class Lcpsave
        {
            public byte CPointer { get; set; }
            public int DatabaseIndex { get; set; }
            public int CardIndex { get; set; }
        }

        private const string DatabaseLocation = @"F:\File\Dokumente\visual studio 2017\Projects\CaH Extractor\CaH Extractor\bin\Debug\DeckDb.json";
        private const string LcpLocation = "cstat.sv";

        private List<DeckContainer> _database;

        public List<DeckContainer> Database => _database;
        public DeckContainer CurrentDeckContainer => _database[_state.DatabaseIndex];

        private bool _unsafed = false;

        private Lcpsave _state;

        public LocalCardProvider()
        {
            _state = File.Exists(LcpLocation) ? JsonConvert.DeserializeObject<Lcpsave>(File.ReadAllText(LcpLocation)) : new Lcpsave();
        }

        public async Task Initialize()
        {
            await Task.Delay(1); // Just to skip ide warnings

            var rawdb = File.ReadAllText(DatabaseLocation);
            _database = JsonConvert.DeserializeObject<List<DeckContainer>>(rawdb);
        }

        public CardInfo Current
        {
            get
            {
                if (_state.CPointer == 0)
                    return CurrentDeckContainer.CallCards[_state.CardIndex];
                else
                    return CurrentDeckContainer.ResponseCards[_state.CardIndex];
            }
        }

        public void RemoveCard()
        {
            if (_state.CPointer == 0)
            {
                CurrentDeckContainer.CallCards.RemoveAt(_state.CardIndex);
                if (CurrentDeckContainer.CallCards.Count < _state.CardIndex)
                {
                    _state.CardIndex = 0;
                    _state.CPointer = 1;
                }
            }
            else
            {
                CurrentDeckContainer.ResponseCards.RemoveAt(_state.CardIndex);
                if (CurrentDeckContainer.ResponseCards.Count < _state.CardIndex)
                {
                    if (_database.Count > _state.DatabaseIndex + 1)
                    {
                        _state.CardIndex = 0;
                        _state.CPointer = 0;
                    }
                }
            }
        }

        public bool MovePrevious()
        {
            if (_state.CardIndex <= 0)
            {
                if (_state.CPointer == 1)
                {
                    _state.CPointer = 0;
                    _state.CardIndex = CurrentDeckContainer.CallCards.Count;
                }
                else
                {
                    if (_state.DatabaseIndex == 0)
                    {
                        return false;
                    }

                    _state.DatabaseIndex--;
                    _state.CPointer = 1;
                    _state.CardIndex = CurrentDeckContainer.ResponseCards.Count;
                }
            }

            _state.CardIndex--;
            return true;
        }

        public bool MoveNext()
        {
            if (_state.CPointer == 0 && CurrentDeckContainer.CallCards.Count - 1 <= _state.CardIndex)
            {
                _state.CPointer = 1;
                _state.CardIndex = 0;
            }
            else if (_state.CPointer == 1 && CurrentDeckContainer.ResponseCards.Count - 1 <= _state.CardIndex)
            {
                _state.CPointer = 0;
                _state.CardIndex = 0;

                if (_database.Count <= ++_state.DatabaseIndex)
                {
                    return false;
                }
            }
            else
            {
                _state.CardIndex++;
            }

            return true;
        }

        public void MarkFlush()
        {
            _unsafed = true;
        }

        public void Flush()
        {
            if (_unsafed)
            {
                _unsafed = false;
                File.WriteAllText(DatabaseLocation, JsonConvert.SerializeObject(_database));
                File.WriteAllText(LcpLocation, JsonConvert.SerializeObject(_state));
            }
        }

        public void Reset(bool resetOverride = false)
        {
            _state.DatabaseIndex = 0;
            _state.CPointer = 0;
            _state.CardIndex = 0;

            foreach (var cInfo in _database.SelectMany(m => m.CallCards).Cast<CardInfo>().Concat(_database.SelectMany(m => m.ResponseCards)))
            {
                cInfo.Picked = false;

                if (!resetOverride) continue;

                cInfo.CardOverride = OverrideCardType.Default;
            }
            _unsafed = true;
        }
    }
}
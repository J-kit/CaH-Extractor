using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaH.Shared.Extensions;
using CaH.Shared.Net;
using CaH.Shared.Poco;

namespace CaH.Shared.Cardcast
{
    public class AsyncCardEnumerator :
        IAsyncEnumerator<DeckSearchInformation>, IEnumerator<DeckSearchInformation>,
        IAsyncEnumerable<DeckSearchInformation>, IEnumerable<DeckSearchInformation> //IEnumerable

    {
        private readonly SearchQuery _sourcequery;

        private SearchQuery _cQuery;

        private CcGameApi _ctx;

        private DeckSearchInformation _current;
        private Queue<DeckSearchInformation> _internalQueue;

        private long _maxOffset;
        private bool _hasExecuted = false;

        public long Count => _maxOffset;

        public AsyncCardEnumerator(CcGameApi context, SearchQuery query)
        {
            _ctx = context;
            _cQuery = query.DeepCopy();
            _sourcequery = query.DeepCopy();

            _internalQueue = new Queue<DeckSearchInformation>();
        }

        IEnumerator<DeckSearchInformation> IEnumerable<DeckSearchInformation>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IAsyncEnumerator<DeckSearchInformation> IAsyncEnumerable<DeckSearchInformation>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private AsyncCardEnumerator GetEnumerator()
        {
            return this; //new AsyncCardEnumerator(_ctx, _sourcequery);
        }

        public void Dispose()
        {
            _cQuery = null;
        }

        public bool MoveNext()
        {
            return MoveNextAsync().Result; //Task.result always halts current thread
        }

        public async Task<bool> MoveNextAsync()
        {
            while (_internalQueue.Count == 0)
            {
                if (_cQuery.Offset >= _maxOffset && _hasExecuted) //Current != null means there has been no execution
                {
                    return false;
                }

                var searchDeck = await _ctx.SearchDeckAsync(_cQuery);
                searchDeck.Results.Data.ForEach(_internalQueue.Enqueue);

                if (!_hasExecuted)
                {
                    //Didn't do any execution yet
                    _maxOffset = searchDeck.Results.Count;
                    _hasExecuted = true;
                }

                _cQuery.Offset += _cQuery.Limit; // searchDeck.Results.Data.Length;
            }

            if (_internalQueue.Count > 0)
            {
                _current = _internalQueue.Dequeue();
                return true;
            }

            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        DeckSearchInformation IEnumerator<DeckSearchInformation>.Current => _current;

        public DeckSearchInformation Current => _current;

        object IEnumerator.Current => _current;
    }
}
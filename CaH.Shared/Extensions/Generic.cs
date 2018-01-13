using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaH.Shared.Poco;
using Newtonsoft.Json;

namespace CaH.Shared.Extensions
{
    public static class Generic
    {
        public static T DeepCopy<T>(this T @object)
        {
            var serialized = JsonConvert.SerializeObject(@object);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static int ForEach<T>(this IEnumerable<T> input, Action<T> toRunAct)
        {
            int iterations = 0;
            if (input == null) return iterations;
            foreach (var put in input)
            {
                if (!put.Equals(default(T)))
                {
                    toRunAct(put);
                    iterations++;
                }
            }
            return iterations;
        }

        public static async Task<int> ForEachAsync<T>(this IAsyncEnumerable<T> input, Action<T> toRunAct)
        {
            int iterations = 0;
            if (input == null) return iterations;

            using (var enumerator = input.GetEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    var val = enumerator.Current;
                    if (!val.Equals(default(T)))
                    {
                        toRunAct(val);
                    }
                }
            }

            return iterations;
        }

        public static async Task<int> ForEachAsync<T>(this IAsyncEnumerable<T> input, Func<T, Task> toRunAct)
        {
            int iterations = 0;
            if (input == null) return iterations;

            using (var enumerator = input.GetEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    var val = enumerator.Current;
                    if (!val.Equals(default(T)))
                    {
                        await toRunAct(val);
                    }
                }
            }

            return iterations;
        }
    }
}
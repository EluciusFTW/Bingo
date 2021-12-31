using System;
using System.Collections.Generic;
using System.Linq;

namespace Bingo.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        internal static IReadOnlyCollection<int> NOutOf(this IReadOnlyCollection<int> source, int nrOfElements)
            => source.PickRandomly(nrOfElements, new Random());

        internal static IReadOnlyCollection<int> PickRandomly(this IReadOnlyCollection<int> source, int nrOfElements, Random randomGenerator)
        {
            var alreadyHit = new List<int>();
            var results = new List<int>();
            while (alreadyHit.Count < nrOfElements)
            {
                var element = randomGenerator.Next(source.Count);
                if (alreadyHit.Contains(element)) continue;

                alreadyHit.Add(element);
                results.Add(source.ElementAt(element));
            }
            return results;
        }
    }
}
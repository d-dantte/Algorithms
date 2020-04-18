using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.AngryAnimals
{
    class Challenge
    {
        public static long angryAnimals(int n, List<int> a, List<int> b)
        {
            var maliceMap = MapMalice(a, b);
            var validIntervalCount = SumOfConsecutives(n) - DistinctMaliciousSubsetCount(maliceMap, n);

            return validIntervalCount;
        }

        private static int SumOfConsecutives(long animalCount) => (int)((animalCount * (animalCount + 1)) / 2);

        private static int DistinctMaliciousSubsetCount(MaliceMap[] maps, int sequenceCount)
        {
            var acc = maps.Aggregate(new Accumulator(), (accumulator, next) =>
            {
                next.SubsetCount = next.DistinctSubsetCount(accumulator.Previous, sequenceCount);
                accumulator.Previous = next;
                accumulator.Cumulative += next.SubsetCount;

                return accumulator;
            });

            return acc.Cumulative;
        }

        private static MaliceMap[] MapMalice(List<int> alist, List<int> blist)
        {
            var stack = new Stack<MaliceMap>();
            alist
                .Select((value, indx) => new MaliceMap(value, blist[indx]))
                .OrderBy(map => map.LowerBound)
                .ForEach(map =>
                {
                    while (stack.TryPeek(out var peeked)
                        && (map.Equals(peeked) || map.IsProperSubsetOf(peeked)))
                    {
                        stack.Pop();
                    }

                    stack.Push(map);
                });

            return stack.Reverse().ToArray();
        }
    }

    class MaliceMap : IEquatable<MaliceMap>
    {
        public int LowerBound { get; set; }

        public int UpperBound { get; set; }

        public int SubsetCount { get; set; }

        public MaliceMap(int value1, int value2)
        {
            UpperBound = Math.Max(value1, value2);
            LowerBound = Math.Min(value1, value2);
        }

        public int DistinctSubsetCount(MaliceMap previous, int animalCount)
        {
            return (LowerBound - (previous?.LowerBound ?? 0)) * ((animalCount + 1) - UpperBound);
        }

        public bool IsProperSubsetOf(MaliceMap maliceMap)
        {
            return maliceMap != null
                && !Equals(maliceMap)
                && maliceMap.LowerBound <= LowerBound
                && maliceMap.UpperBound >= UpperBound;
        }

        public bool Equals(MaliceMap other)
        {
            if (other == null)
                return false;

            else
                return other.UpperBound == UpperBound
                    && other.LowerBound == LowerBound;
        }

        public override string ToString() => $"[{LowerBound}, {UpperBound}]";
    }

    class Accumulator
    {
        public MaliceMap Previous { get; set; }
        public int Cumulative { get; set; }
    }

    static class Extensions
    {
        public static bool TryPeek(this Stack<MaliceMap> maps, out MaliceMap map)
        {
            if (maps == null || maps.Count == 0)
            {
                map = null;
                return false;
            }
            else
            {
                map = maps.Peek();
                return true;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enm, Action<T> action)
        {
            foreach (var t in enm)
                action.Invoke(t);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Codility01.PoisonInterval
{
    class Challenge
    {
        public static void Read(string values, out int specimenCount, out List<int> allergic, out List<int> poisonous)
        {
            var reader = new StringReader(values);
            specimenCount = int.Parse(reader.ReadLine());

            var capacity = int.Parse(reader.ReadLine());
            allergic = new List<int>(capacity);

            for (int cnt = 0; cnt < capacity; cnt++)
                allergic.Add(int.Parse(reader.ReadLine()));

            capacity = int.Parse(reader.ReadLine());
            poisonous = new List<int>(capacity);

            for (int cnt = 0; cnt < capacity; cnt++)
                poisonous.Add(int.Parse(reader.ReadLine()));

            string x;
            if ((x = reader.ReadLine()) != null)
                throw new Exception($"Something went wrong: extra value found '{x}'");
        }

        public static long bioHazard(int n, List<int> allergic, List<int> poisonous)
        {
            var hazardMaps = MapHazards(allergic, poisonous);
            var validIntervalCount = SumOfConsecutives(n) - DistinctHazardousSubsetCount(hazardMaps, n);

            return validIntervalCount;
        }

        private static int SumOfConsecutives(long specimenCount) => (int)((specimenCount * (specimenCount + 1)) / 2);

        private static int DistinctHazardousSubsetCount(PoisonMap[] maps, int sequenceCount)
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

        private static PoisonMap[] MapHazards(List<int> allergic, List<int> poisonous)
        {
            var stack = new Stack<PoisonMap>();
            allergic
                .Select((value, indx) => new PoisonMap(value, poisonous[indx]))
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

    class PoisonMap: IEquatable<PoisonMap>
    {
        public int LowerBound { get; set; }

        public int UpperBound { get; set; }

        public int SubsetCount { get; set; }

        public PoisonMap(int value1, int value2)
        {
            UpperBound = Math.Max(value1, value2);
            LowerBound = Math.Min(value1, value2);
        }

        public int DistinctSubsetCount(PoisonMap previous, int specimenCount)
        {
            return (LowerBound - (previous?.LowerBound ?? 0)) * ((specimenCount + 1) - UpperBound);
        }

        public bool IsProperSubsetOf(PoisonMap poisonMap)
        {
            return poisonMap != null
                && !Equals(poisonMap)
                && poisonMap.LowerBound <= LowerBound
                && poisonMap.UpperBound >= UpperBound;
        }

        public bool Equals(PoisonMap other)
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
        public PoisonMap Previous { get; set; }
        public int Cumulative { get; set; }
    }

    static class Extensions
    {
        public static bool TryPeek(this Stack<PoisonMap> maps, out PoisonMap map)
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

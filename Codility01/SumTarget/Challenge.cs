using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.SumTarget
{
	public class Challenge
	{
        static public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var candidateSet = new HashSet<int>(candidates);
            candidates = candidates
                .OrderByDescending(c => c)
                .Where(c => c <= target)
                .ToArray();

            var all = Enumerable
                .Range(0, candidates.Length)
                .SelectMany(candidateSize => Combinations(candidates.Skip(candidateSize).ToArray(), target))
                .Where(list => list.Count > 0)
                .Select(list => list.OrderBy(l => l).ToList())
                .Distinct(new ListComparer())
                .ToList();

            return all;
        }

        static IList<IList<int>> Combinations(int[] candidateList, int target)
        {
            if (candidateList.Length == 0)
                return new List<IList<int>>();

            var candidate = candidateList.First();
            var multiple = target / candidate;
            List<int> primaryCombination = new List<int>();
            IList<IList<int>> validCombinations = new List<IList<int>>();
            var sum = 0;

            for (int cnt = 0; cnt < multiple; cnt++)
            {
                var newSum = sum + candidate;
                if (newSum < target)
                {
                    primaryCombination.Add(candidate);
                    sum = newSum;

                    for (int cntt = 1; cntt < candidateList.Length; cntt++)
                    {
                        var secondaryCombinations = Combinations(candidateList.Skip(cntt).ToArray(), target - newSum);

                        if (secondaryCombinations.Count > 0)
                        {
                            foreach (var list in secondaryCombinations)
                            {
                                validCombinations.Add(primaryCombination.Concat(list).ToList());
                            }
                        }
                    }
                }
                else if (newSum == target)
                {
                    primaryCombination.Add(candidate);
                    sum = newSum;

                    validCombinations.Add(primaryCombination);
                }
            }

            if(validCombinations.Count == 0)
            {
                foreach(var list in Combinations(candidateList.Skip(1).ToArray(), target))
                {
                    validCombinations.Add(list);
                }
            }

            return validCombinations;
        }


        public class ListComparer : IEqualityComparer<IList<int>>
        {
            public bool Equals(IList<int> x, IList<int> y) => x.SequenceEqual(y);

            public int GetHashCode(IList<int> obj)
            {
                return obj.Aggregate(3, (acc, next) => acc * 5 + next.GetHashCode());
            }
        }
    }
}

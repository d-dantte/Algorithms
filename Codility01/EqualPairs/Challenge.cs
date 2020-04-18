using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.EqualPairs
{
    class Challenge
    {
        public int Solution(int[] A)
        {
            var table = new Dictionary<int, List<int>>();

            //build a map of values and where they occur
            for (int cnt = 0; cnt < A.Length; cnt++)
            {
                var value = A[cnt];
                if (!table.TryGetValue(value, out var list))
                    list = table[value] = new List<int>();

                list.Add(cnt);
            }

            //calculate pair count

            return table
                .Where(kvp => kvp.Value.Count > 1)
                .Select(PairCount)
                .Sum();
        }

        public int PairCount(KeyValuePair<int, List<int>> keyValuePair)
        {
            var n = keyValuePair.Value.Count - 1;
            return (n * (1 + n)) / 2;
        }
    }
}

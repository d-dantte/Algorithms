using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.PairSum
{
	public class Challenge
	{
		public IList<IList<int>> ClosestPair(int[][] a, int[][] b, int target)
		{
			var bdict = new SortedList<int, int[][]>(b
				.GroupBy(ar => ar[1])
				.ToDictionary(g => g.Key, g => g.ToArray()));

			var adict = new SortedList<int, int[][]>(a
				.GroupBy(ar => ar[1])
				.ToDictionary(g => g.Key, g => g.ToArray()));

			var idPairs = new List<List<int>>();
			int? distance = null;
			foreach(var keyA in adict.Keys)
			{
				if (keyA > target)
					continue;

				var keyB = target - keyA;
				if (bdict.ContainsKey(keyB))
				{
					if (distance != 0)
						idPairs.Clear();

					distance = 0;
					foreach (var pairA in adict[keyA])
						foreach (var pairB in bdict[keyB])
							idPairs.Add(new List<int> { pairA[0], pairB[0] });
				}
				else if (distance == null || (target - (keyA + keyB)) < distance)
				{
					idPairs.Clear();

					distance = target - keyA + keyB;
					foreach (var pairA in adict[keyA])
						foreach (var pairB in bdict[keyB])
							idPairs.Add(new List<int> { pairA[0], pairB[0] });
				}
			}
			throw new Exception();
		}
	}
}

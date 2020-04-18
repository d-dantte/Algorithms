using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.Permutation
{
	public class Challenge
	{
		static public IList<IList<int>> Permutation(int[] values)
		{
			return Permutate(values);
		}

		static public IList<IList<int>> Permutate(int[] values)
		{
			if (values.Length == 1)
				return new List<IList<int>>(new[] { values });

			else
			{
				//var visited = new HashSet<int>();
				return values
					//.Where(value => visited.Add(value))
					.SelectMany((value, index) =>
					{
						var primary = new[] { value };
						return Permutate(Splice(values, index))
							.Select(perm =>
							{
								return primary.Concat(perm).ToList() as IList<int>;
							});
					})
					.ToList();
			}
		}

		static private int[] Splice(int[] list, int index)
		{
			if (index >= list.Length)
				throw new Exception();

			else return list
				.Take(index)
				.Concat(list.Skip(index + 1))
				.ToArray();
		}
	}

	public class Solution
	{

		public IList<IList<int>> Permutation(int[] values)
		{
			return Permutate(values);
		}

		public IList<IList<int>> Permutate(int[] values)
		{
			if (values.Length == 1)
				return new List<IList<int>>(new[] { values });

			else
			{
				//var visited = new HashSet<int>();
				return values
					//.Where(value => visited.Add(value))
					.SelectMany((value, index) =>
					{
						var primary = new[] { value };
						return this.Permutate(Splice(values, index))
							.Select(perm =>
							{
								return primary.Concat(perm).ToList() as IList<int>;
							});
					})
					.ToList();
			}
		}

		private int[] Splice(int[] list, int index)
		{
			if (index >= list.Length)
				throw new Exception();

			else return list
				.Take(index)
				.Concat(list.Skip(index + 1))
				.ToArray();
		}
	}
}

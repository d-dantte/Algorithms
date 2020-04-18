using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.ArrayMedian
{
	class Challenge
	{
		public static IEnumerable<int> MergeArrays(int[] nums1, int[] nums2)
		{
			int[] result = new int[nums1.Length + nums2.Length];
			int i = 0; int j = 0;
			while (i < nums1.Length && j < nums2.Length)
			{
				if (nums1[i] < nums2[j])
				{
					yield return nums1[i++];
				}
				else
				{
					yield return nums2[j++];
				}
			}

			while (i < nums1.Length)
			{
				yield return nums1[i++];
			}

			while (j < nums2.Length)
			{
				yield return nums2[j++];
			}
		}

		public static double Median(IEnumerable<int> sortedArray, int size)
		{
			var enumerator = sortedArray.GetEnumerator();

			if (size % 2 == 0)
			{
				var middle = size / 2;

				var elements = new[] { 0, 0 };

				int i = 0;
				while (enumerator.MoveNext())
				{
					if (i == middle - 1)
					{
						elements[0] = enumerator.Current;
					}
					if (i == middle)
					{
						elements[1] = enumerator.Current;
						break;
					}
					i++;
				}

				return (elements[0] + elements[1]) / 2.0;
			}
			else
			{
				var middle = size / 2.0;
				int i = 0;
				while (enumerator.MoveNext())
				{
					if (i == middle)
					{
						return enumerator.Current;
					}
					i++;
				}
				throw new Exception("Invalid Middle Value");
			}
		}
	}
}

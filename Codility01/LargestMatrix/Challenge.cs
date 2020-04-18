using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.LargestMatrix
{
	class Challenge
	{
		public static int largestMatrix(List<List<int>> arr)
		{
			var largestSquare = 0;
			for (int cnty = 0; cnty < arr.Count; cnty++) 
			{
				for (int cntx = 0; cntx < arr.Count; cntx++)
				{
					if(arr[cnty][cntx] != 0 && CanHaveLargerSquare(cntx, cnty, largestSquare, arr.Count))
					{
						var squareSize = LargestSquareSize(cntx, cnty, arr);

						largestSquare = squareSize > largestSquare ? squareSize : largestSquare;
					}
				}
			}

			return largestSquare;
		}

		public static int LargestSquareSize(int x, int y, List<List<int>> array)
		{
			var maxLength = Math.Min(array.Count - x, array.Count - y);
			int largestSquareSize = 0;
			for (int count = 0; count < maxLength; count++)
			{
				if (!IsSquare(x + count, y + count, count, array))
					break;

				else largestSquareSize++;
			}

			return largestSquareSize;
		}

		public static bool IsSquare(int x, int y, int length, List<List<int>> array)
		{
			var miny = y - length;
			for (int cnty = y; cnty >= miny; cnty--)
			{
				if (array[cnty][x] != 1)
					return false;
			}

			var minx = x - length;
			for (int cntx = x; cntx >= minx; cntx--)
			{
				if (array[y][cntx] != 1)
					return false;
			}

			return true;
		}

		public static bool CanHaveLargerSquare(int x, int y, int largestSquare, int count)
		{
			return largestSquare < (count - x)
				&& largestSquare < (count - y);
		}


	}

	class Square
	{
		public int StartX { get; set; }

		public int StartY { get; set; }

		public int Length { get; set; }
	}
}

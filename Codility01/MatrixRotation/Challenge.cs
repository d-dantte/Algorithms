using System;
using System.Collections.Generic;
using System.Text;

namespace Codility01.MatrixRotation
{
	public class Challenge
	{
		public static int[][] RotateMatrixAntiClockwise(int[][] array)
		{
			FlipAroundColumn(array);
			RotateAlongLeftDiagonal(array);

			return array;
		}

		public static int[][] RotateMatrixClockwise(int[][] array)
		{
			FlipAroundColumn(array);
			RotateAlongRightDiagonal(array);

			return array;
		}

		public static int[][] FlipAroundColumn(int[][] array)
		{
			var lim = array.GetLength(0) / 2;
			var arrayLength = array.GetLength(0);
			for (int row = 0; row < arrayLength; row++)
			{
				for (int col = 0; col < lim; col++)
				{
					var temp = array[row][col];
					array[row][col] = array[row][arrayLength - 1 - col];
					array[row][arrayLength - 1 - col] = temp;
				}
			}

			return array;
		}

		public static int[][] FlipAroundRow(int[][] array)
		{
			var lim = array.GetLength(0) / 2;
			var arrayLength = array.GetLength(0);
			for (int col = 0; col < arrayLength; col++)
			{
				for (int row = 0; row < lim; row++)
				{
					var temp = array[row][col];
					array[row][col] = array[row][arrayLength - 1 - col];
					array[row][arrayLength - 1 - col] = temp;
				}
			}

			return array;
		}

		public static int[][] RotateAlongLeftDiagonal(int[][] array)
		{
			for (int row = 0; row < array.GetLength(0); row++)
			{
				for(int col = row; col < array.GetLength(0); col++)
				{
					if (col == row)
						continue;

					var temp = array[row][col];
					array[row][col] = array[col][row];
					array[col][row] = temp;
				}
			}
			return array;
		}

		public static int[][] RotateAlongRightDiagonal(int[][] array)
		{
			for(int row = 0; row < array.GetLength(0); row++)
			{
				var txrow = Transform(row, array.GetLength(0));
				for(int col = row; col < array.GetLength(0); col++)
				{
					if (col == row)
						continue;

					var txcol = Transform(col, array.GetLength(0));
					var temp = array[txrow][col];
					array[txrow][col] = array[txcol][row];
					array[txcol][row] = temp;

				}
			}

			return array;
		}

		private static int Transform(int value, int arrayLength)
		{
			return arrayLength - 1 - value;
		}


		public static string ToString(int[][] array)
		{
			var sb = new StringBuilder();
			for(int row = 0; row < array.GetLength(0); row++)
			{
				sb.Append(string.Join(", ", array[row])).AppendLine();
			}

			return sb.ToString();
		}
	}
}

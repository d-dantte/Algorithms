using Codility01.Helpers.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.IslandCount
{
	public static class Challenge
	{
		public static int Solve(char[][] grid)
		{
			var points = new List<Point>();
			for(int cntr = 0; cntr < grid.Length; cntr ++)
			{
				for(int cntc = 0; cntc < grid[cntr].Length; cntc++)
				{
					points.Add(new Point
					{
						Row = cntr,
						Column = cntc,
						Length = grid[cntr].Length
					});
				}
			}

			var unionFind = new UnionFind<Point>(points, point => point.Biject());

			for(int cntr = 0; cntr < grid.Length; cntr ++)
			{
				for(int cntc = 0; cntc < grid[cntr].Length; cntc ++)
				{
					var ch = grid[cntr][cntc];
					if (ch == '1')
					{
						var point = new Point { Column = cntc, Row = cntr, Length = grid[cntr].Length };
						if (TryGetValidLeftPoint(cntr, cntc, grid, out var leftPoint))
							unionFind.Union(point, leftPoint);

						if (TryGetValidTopPoint(cntr, cntc, grid, out var topPoint))
							unionFind.Union(point, topPoint);
					}
				}
			}

			return unionFind
				.Groups()
				.Count(g => g.Data.First().Char(grid) == '1');
		}

		public static bool TryGetValidLeftPoint(int row, int col, char[][] grid, out Point point)
		{
			point = null;
			if (col == 0)
				return false;

			var lp = grid[row][col - 1];
			if (lp != '1')
				return false;

			point = new Point { Column = col - 1, Row = row, Length = grid[row].Length };
			return true;
		}

		public static bool TryGetValidTopPoint(int row, int col, char[][] grid, out Point point)
		{
			point = null;
			if (row == 0)
				return false;

			var tp = grid[row - 1][col];
			if (tp != '1')
				return false;

			point = new Point { Column = col, Row = row - 1, Length = grid[row].Length };
			return true;
		}

		public class Point
		{
			public int Row { get; set; }
			public int Column { get; set; }
			public int Length { get; set; }

			public override string ToString() => $"[row:{Row}, col:{Column}, Len:{Length}, Biject: {Biject()}]";

			public int Biject() => (Row * Length) + Column;

			public char Char(char[][] grid) => grid[Row][Column];
		}
	}
}

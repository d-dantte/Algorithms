using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.OceanFlow2
{
	public class Challenge2
	{
		public static IList<IList<int>> PacificAtlantic(int[][] matrix)
		{
			var tmp = Enumerable
				.Range(0, matrix.Length)
				.Select(r => new Coord { R = r, C = 0 });

			tmp = tmp.Concat(Enumerable
				.Range(1, matrix[0].Length - 1)
				.Select(c => new Coord { R = 0, C = c }));

			var pacificEdges = tmp.ToArray();

			var lrindex = matrix.Length - 1;
			tmp = Enumerable
				.Range(0, matrix.Length)
				.Select(r => new Coord { R = lrindex - r, C = matrix[lrindex-r].Length - 1 });

			var lcindex = matrix[lrindex].Length - 1;
			tmp = tmp.Concat(Enumerable
				.Range(0, matrix[lrindex].Length)
				.Select(c => new Coord { R = lrindex, C = lcindex - c }));

			var atlanticEdges = tmp.ToArray();

			//atlantic points
			var atlanticContext = new Context(matrix);
			foreach(var coord in atlanticEdges)
			{
				if(!atlanticContext.Traversed.Contains(coord))
					FindPeak(new[] { coord }, atlanticContext);
			}

			var pacificContext = new Context(matrix);
			foreach(var coord in pacificEdges)
			{
				if(!pacificContext.Traversed.Contains(coord))
					FindPeak(new[] { coord }, pacificContext);
			}

			return atlanticContext.Traversed
				.Intersect(pacificContext.Traversed)
				.Select(p => p.ToList())
				.ToList();
		}

		public static void FindPeak(IEnumerable<Coord> bfs, Context context)
		{
			foreach (var coord in bfs)
				context.Traversed.Add(coord);

			//gather next level
			var set = new HashSet<Coord>();
			foreach(var coord in bfs)
			{
				var nextcoords = NextCoords(coord, context);
				foreach (var c in nextcoords)
					set.Add(c);
			}

			if (set.Count() > 0)
				FindPeak(set, context);
		}

		public static Coord[] NextCoords(Coord coord, Context context)
		{
			var list = new List<Coord>();

			//left
			var tmp = coord.C - 1;
			if (tmp >= 0
				&& !context.IsTraversed(coord.R, tmp)
				&& context.ValueOf(coord.R, tmp) >= context.ValueOf(coord.R, coord.C))
				list.Add(new Coord { C = tmp, R = coord.R });

			//right
			tmp = coord.C + 1;
			if (tmp < context.Matrix[coord.R].Length
				&& !context.IsTraversed(coord.R, tmp)
				&& context.ValueOf(coord.R, tmp) >= context.ValueOf(coord.R, coord.C))
				list.Add(new Coord { C = tmp, R = coord.R });

			//top
			tmp = coord.R - 1;
			if (tmp >= 0
				&& !context.IsTraversed(tmp, coord.C)
				&& context.ValueOf(tmp, coord.C) >= context.ValueOf(coord.R, coord.C))
				list.Add(new Coord { C = coord.C, R = tmp });

			//right
			tmp = coord.R + 1;
			if (tmp < context.Matrix.Length
				&& !context.IsTraversed(tmp, coord.C)
				&& context.ValueOf(tmp, coord.C) >= context.ValueOf(coord.R, coord.C))
				list.Add(new Coord { C = coord.C, R = tmp });

			return list.ToArray();
		}
	}

	public class Context
	{
		public HashSet<Coord> Traversed { get; } = new HashSet<Coord>();
		//public HashSet<Coord> Peaks { get; } = new HashSet<Coord>();

		public int[][] Matrix { get; }

		public Context(int[][] matrix)
		{
			Matrix = matrix;
		}

		public bool IsTraversed(int r, int c)
			=> Traversed.Contains(new Coord { R = r, C = c });

		public int ValueOf(int r, int c) => Matrix[r][c];
	}

	public class Coord
	{
		public int C { get; set; }
		public int R { get; set; }

		public IList<int> ToList() => new List<int> { R, C };

		public override string ToString() => $"{R},{C}";

		public override bool Equals(object obj)
		{
			return obj is Coord other
				&& other.C == C
				&& other.R == R;
		}

		public override int GetHashCode()
		{
			var hash = 3 * 5 + R;
			return hash * 7 + C;
		}
	}
}

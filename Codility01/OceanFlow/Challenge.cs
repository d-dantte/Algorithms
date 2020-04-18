using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.OceanFlow
{
	public class Challenge
    {
        public static IList<IList<int>> PacificAtlantic(int[][] matrix)
        {
            var coords = matrix
                .SelectMany((r, ri) => r
                .Select((cv, ci) => new Coord { R = ri, C = ci, Value = matrix[ri][ci] }))
                .OrderByDescending(p => p.Value)
                .ToArray();

            var flowcoords = new HashSet<Coord>();
            var context = new Context { Matrix = matrix };

            foreach (var coord in coords)
            {
                if(!context.IsDeadPath(coord.R, coord.C))
                    FindFlowPath(coord, new HashSet<Coord>(), context.Start(coord));
            }


            return context.PacificPaths.Keys
                .Intersect(context.AtlanticPaths.Keys)
                .Select(p => p.ToArray())
                .ToList();
        }

        private static bool FindFlowPath(
            Coord coord,
            HashSet<Coord> traversed,
            Context context)
        {
            traversed.Add(coord);

            //if we are at either edges
            bool foundA = false;
            bool foundP = false;
            if (IsAtlanticCoord(coord, context.Matrix) || context.IsAtlanticPath(coord))
            {
                foundA = true;
                context.AddAtlanticPaths(traversed);
            }

            if (IsPacificCoord(coord, context.Matrix) || context.IsPacificPath(coord))
            {
                foundP = true;
                context.AddPacificPaths(traversed);
            }

            bool foundC = false;
            if (!(foundA && foundP))
            {
                var nextpaths = NextPaths(coord, traversed, context);
                if (nextpaths.Length > 0)
                {
                    foreach (var np in nextpaths)
                    {
                        bool r;
                        if (!(r = FindFlowPath(np, traversed, context)))
                            context.AddDeadPath(np);

                        foundC |= r;
                    }
                }
            }

            traversed.Remove(coord);
            return foundA || foundP || foundC;
        }

        private static Coord[] NextPaths(
            Coord coord,
            HashSet<Coord> traversed,
            Context context)
        {
            var coords = new List<Coord>();

            //left
            var tmp = coord.C - 1;
            if (tmp >= 0
               && !context.IsDeadPath(coord.R, tmp)
               && context.Matrix[coord.R][tmp] <= coord.Value
               && !traversed.Contains(coord.FromColumn(tmp, context.Matrix)))
                coords.Add(coord.FromColumn(tmp, context.Matrix));

            //right
            tmp = coord.C + 1;
            if (tmp < context.Matrix[coord.R].Length
               && !context.IsDeadPath(coord.R, tmp)
               && context.Matrix[coord.R][tmp] <= coord.Value
               && !traversed.Contains(coord.FromColumn(tmp, context.Matrix)))
                coords.Add(coord.FromColumn(tmp, context.Matrix));

            //top
            tmp = coord.R - 1;
            if (tmp >= 0
               && !context.IsDeadPath(tmp, coord.C)
               && context.Matrix[tmp][coord.C] <= coord.Value
               && !traversed.Contains(coord.FromRow(tmp, context.Matrix)))
                coords.Add(coord.FromRow(tmp, context.Matrix));

            //bottom
            tmp = coord.R + 1;
            if (tmp < context.Matrix.Length
               && !context.IsDeadPath(tmp, coord.C)
               && context.Matrix[tmp][coord.C] <= coord.Value
               && !traversed.Contains(coord.FromRow(tmp, context.Matrix)))
                coords.Add(coord.FromRow(tmp, context.Matrix));

            return coords.ToArray();
        }

        private static bool IsAtlanticCoord(Coord p, int[][] matrix)
        {
            return p.R == matrix.Length - 1
                || p.C == matrix[p.R].Length - 1;
        }

        private static bool IsPacificCoord(Coord p, int[][] matrix)
        {
            return p.R == 0
                || p.C == 0;
        }
    }

    public class Context
    {
        public Dictionary<Coord, HashSet<Coord>> PacificPaths { get; } = new Dictionary<Coord, HashSet<Coord>>();
        public Dictionary<Coord, HashSet<Coord>> AtlanticPaths { get; } = new Dictionary<Coord, HashSet<Coord>>();

        public HashSet<Coord> DeadPaths { get; } = new HashSet<Coord>();

        public Coord StartCoord { get; private set; }

        public int[][] Matrix { get; set; }

        public Context Start(Coord start)
        {
            StartCoord = start;
            return this;
        }

        public void AddDeadPath(Coord p)
        {
            DeadPaths.Add(p);
        }

        public bool IsDeadPath(int r, int c)
        {
            return DeadPaths.Contains(new Coord { R = r, C = c });
        }

        public void AddAtlanticPaths(HashSet<Coord> traversed)
        {
            if (!AtlanticPaths.ContainsKey(StartCoord))
                AtlanticPaths[StartCoord] = new HashSet<Coord>();

            foreach (var p in traversed)
                AtlanticPaths[StartCoord].Add(p);
        }

        public void AddPacificPaths(HashSet<Coord> traversed)
        {
            if (!PacificPaths.ContainsKey(StartCoord))
                PacificPaths[StartCoord] = new HashSet<Coord>();

            foreach (var p in traversed)
                PacificPaths[StartCoord].Add(p);
        }

        public bool IsAtlanticPath(Coord p)
        {
            foreach (var paths in AtlanticPaths.Values)
                if (paths.Contains(p)) return true;

            return false;
        }

        public bool IsPacificPath(Coord p)
        {
            foreach (var paths in PacificPaths.Values)
                if (paths.Contains(p)) return true;

            return false;
        }

    }

    public class Coord
    {
        public int C { get; set; }
        public int R { get; set; }

        public int? Value { get; set; }

        public override string ToString() => $"{R},{C}";

        public IList<int> ToArray() => new List<int> { R, C };

        public Coord FromRow(int row, int[][] matrix) => new Coord
        {
            R = row,
            C = C,
            Value = matrix[row][C]
        };

        public Coord FromColumn(int col, int[][] matrix) => new Coord
        {
            R = R,
            C = col,
            Value = matrix[R][col]
        };

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

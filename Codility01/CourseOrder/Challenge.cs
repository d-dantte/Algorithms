using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.CoursesOrder
{
    public class Challenge
    {
        public static int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            return Solve(numCourses, prerequisites);
        }

        public static int[] Solve(int numCourses, int[][] prerequisites)
        {
            var nodes = prerequisites
                .GroupBy(g => g[0])
                .ToDictionary(g => g.Key, g => g.Select(a => a[1]).ToArray());

            var tmap = new bool?[numCourses];
            var stack = new Stack<int>();

            for (int n = 0; n < numCourses; n++)
            {
                if (tmap[n] == null && !Dfs(n, stack, tmap, nodes))
                    return new int[0];
            }

            if (tmap.Any(t => t == null))
                return new int[0];

            return stack.ToArray();
        }


        public static bool Dfs(
            int node,
            Stack<int> stack,
            bool?[] tmap,
            Dictionary<int, int[]> nodes)
        {
            tmap[node] = true;

            if (nodes.ContainsKey(node))
            {
                foreach (var c in nodes[node].OrderBy(n => n))
                {
                    if (tmap[c] == null && !Dfs(c, stack, tmap, nodes))
                        return false;

                    else if (tmap[c] == true)
                        return false;

                    //ignore when tmap[c] == false
                }
            }

            stack.Push(node);

            tmap[node] = false;

            return true;
        }
    }
}

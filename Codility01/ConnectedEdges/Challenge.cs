using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.ConnectedEdges
{
    public class Challenge
    {
        static public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            if (connections == null
               || connections.Count == 0
               || connections.Any(e => e == null))
                return new List<IList<int>>();

            //build nodes and edges
            var nodes = new Dictionary<int, Node>();
            var edges = new Dictionary<string, Edge>();
            foreach (var cedge in connections)
            {
                if (!TryGet(edges, Edge.GetId(cedge[0], cedge[1]), out var edge))
                {
                    edge = new Edge(cedge);
                    edges[edge.Id] = edge;

                    //from node
                    if (!TryGet(nodes, edge.From, out var node))
                        nodes[edge.From] = node = new Node(edge.From);

                    node.Edges.Add(edge);

                    //To node
                    if (!TryGet(nodes, edge.To, out node))
                        nodes[edge.To] = node = new Node(edge.To);

                    node.Edges.Add(edge);
                }
            }

            //start anywhere
            var first = nodes.Values.First();
            Traverse(null, first, new DFSContext { Step = 1, Nodes = nodes });

            return edges.Values
                .Where(edge => edge.IsBridge)
                .Select(edge => edge.ToList() as IList<int>)
                .ToList();
        }

        static public int Traverse(
            Node parent,
            Node node,
            DFSContext context)
        {
            int? lowestConnection = null;
            node.VisitStep = context.Step;
            foreach (var edge in node.Edges)
            {
                var to = context.Nodes[edge.OtherNode(node.Id)];
                if (to == parent || to.VisitStep > node.VisitStep)
                    continue;

                if (to.VisitStep < node.VisitStep)
                    lowestConnection = lowestConnection == null || to.VisitStep < lowestConnection
                        ? to.VisitStep
                        : lowestConnection;

                else
                {
                    var lowest = Traverse(node, to, context.Advance());
                    if(lowest > node.VisitStep)
                        edge.IsBridge = true;

                    lowestConnection = lowestConnection == null || lowest < lowestConnection
                        ? lowest
                        : lowestConnection;
                }
            }

            if (!lowestConnection.HasValue)
                return node.VisitStep.Value;

            else return lowestConnection.Value;
        }

        static public bool TryGet<K, V>(Dictionary<K, V> dict, K key, out V value)
        {
            if (dict.ContainsKey(key))
            {
                value = dict[key];
                return true;
            }
            else
            {
                value = default(V);
                return false;
            }
        }
    }

    public class DFSContext
    {
        public Dictionary<int, Node> Nodes { get; set; }

        public int Step { get; set; }

        public DFSContext Advance()
        {
            Step += 1;
            return this;
        }
    }

    public class Edge
    {
        public int From { get; }

        public int To { get; }

        public bool IsBridge { get; set; }

        public string Id => GetId(From, To);


        public Edge(IList<int> edge)
        {
            To = Math.Max(edge[0], edge[1]);
            From = Math.Min(edge[0], edge[1]);
        }

        public IList<int> ToList() => new List<int> { From, To };

        public int OtherNode(int nodeId)
        {
            if (nodeId == From)
                return To;

            else if (nodeId == To)
                return From;

            else throw new Exception();
        }

        public override bool Equals(object obj)
        {
            return obj is Edge edge
                && edge.From == From
                && edge.To == To;
        }

        public override int GetHashCode()
        {
            var acc = 3 * 5 + From;
            return acc * 13 + To;
        }

        public static string GetId(int a, int b)
        {
            var min = Math.Min(a, b);
            var max = Math.Max(a, b);

            return $"{min},{max}";
        }
    }

    public class Node
    {
        public int Id { get; }

        public int? VisitStep { get; set; } = null;

        public HashSet<Edge> Edges { get; } = new HashSet<Edge>();

        public Node(int id)
        {
            Id = id;
        }
    }
}

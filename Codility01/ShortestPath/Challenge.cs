using System.Collections.Generic;
using System.Linq;

namespace Codility01.ShortestPath
{
	class Challenge
	{
		public static long DijkistraShortestDistance(List<WeightedEdge> edges, int startNode, int endNode)
		{
			var nodes = DerivePrimedNodes(edges, startNode);
			var queue = new HashSet<Node<WeightedEdge>>(nodes.Values);

			while(queue.Count > 0)
			{
				var node = queue
					.OrderBy(n => n.DistanceFromSource)
					.First();

				queue.Remove(node);

				foreach(var edge in node.Edges)
				{
					var distance = node.DistanceFromSource + edge.Weight;
					if (distance < nodes[edge.End].DistanceFromSource)
						nodes[edge.End].DistanceFromSource = distance;
				}
			}

			return nodes[endNode].DistanceFromSource;
		}

		public static long UnweightedGraphShortestDistance(List<Edge> edges, int startNode, int endNode)
		{
			var nodes = DerivePrimedNodes(edges, startNode);
			var start = nodes[startNode];
			var breathFirst = new List<Node<Edge>>{ start };
			var visited = new HashSet<int> { startNode };
			var distance = 0;

			while(breathFirst.Count > 0)
			{
				//move a step forward
				breathFirst = breathFirst
					.SelectMany(node => node.Edges.Select(edge => edge.Start))
					.Where(nodeId => !visited.Contains(nodeId))
					.Distinct()
					.Select(nodeId => nodes[nodeId])
					.ToList();

				if (breathFirst.Count > 0)
				{
					//every node in "breathFirst" has a total distance from the start of distance+1;
					distance++;

					//add the new nodes to the visited list
					breathFirst.ForEach(node => visited.Add(node.Id));

					if (visited.Contains(endNode))
						return distance;
				}
			}

			return -1;
		}


		private static Dictionary<int, Node<TEdge>> DerivePrimedNodes<TEdge>(
			IEnumerable<TEdge> edges, 
			int startNode) 
			where TEdge: Edge
		{
			var nodes = new Dictionary<int, Node<TEdge>>();
			foreach(var edge in edges)
			{
				//add nodes
				if (!nodes.ContainsKey(edge.End))
					nodes[edge.End] = new Node<TEdge>(
						id: edge.End,
					 	distanceFromSource: startNode == edge.End ? 0 : long.MaxValue);

				Node<TEdge> node;
				if (!nodes.ContainsKey(edge.Start))
					nodes[edge.Start] = node = new Node<TEdge>(
						id: edge.Start,
						distanceFromSource: startNode == edge.Start ? 0 : long.MaxValue);

				else
					node = nodes[edge.Start];

				//add edge to node
				node.Edges.Add(edge);
			}

			return nodes;
		}
	}

	public class Edge
	{
		public int Start { get; }

		public int End { get; }

		public Edge(int start, int end)
		{
			Start = start;
			End = end;
		}
	}

	public class WeightedEdge: Edge
	{
		public int Weight { get; }

		public WeightedEdge(int start, int end, int weight)
			:base(start, end)
		{
			Weight = weight;
		}
	}

	public class Node<TEdge> where TEdge: Edge
	{
		public List<TEdge> Edges { get; } = new List<TEdge>();

		public long DistanceFromSource { get; set; }

		public int Id { get; }


		public Node(int id, long distanceFromSource, params TEdge[] edges)
		{
			Id = id;
			DistanceFromSource = distanceFromSource;
			Edges.AddRange(edges);
		}

		public override bool Equals(object obj)
		{
			return obj is Node<TEdge> node
				&& node.Id == Id
				&& node.DistanceFromSource == DistanceFromSource;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}

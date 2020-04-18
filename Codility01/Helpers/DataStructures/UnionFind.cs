using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility01.Helpers.DataStructures
{
	public class UnionFind<TData>
	{
		private readonly Dictionary<int, Entry> _datamap;
		private readonly Func<TData, int> _bijectionMap;

		public UnionFind(IEnumerable<TData> datalist, Func<TData, int> bijectionMap)
		{
			_bijectionMap = bijectionMap ?? throw new ArgumentNullException(nameof(bijectionMap));
			_datamap = datalist
				.Select(data => new Entry
				{
					Data = data,
					Group = _bijectionMap.Invoke(data)
				})
				.ToDictionary(info => info.Group, info => info);
		}

		public bool Union(TData source, TData destination)
		{
			if (!TryGetRootEntry(source, out var sourceRoot))
				throw new Exception("entry not found");

			if (!TryGetRootEntry(destination, out var destinationRoot))
				throw new Exception("entry not found");

			if (destinationRoot == sourceRoot)
				return false;

			destinationRoot.Children.Add(sourceRoot);
			destinationRoot.Flatten();

			return true;
		}

		public int DistinctGroupCount() => DistinctGroups().Length;

		public int[] DistinctGroups()
		{
			return _datamap.Values
				   .Select(entry => entry.Group)
				   .Distinct()
				   .ToArray();
		}

		public Entry[] Entries() => _datamap.Values.ToArray();

		public int GroupOf(TData data) => _datamap[_bijectionMap.Invoke(data)].Group;

		public Group[] Groups() => _datamap.Values
			.GroupBy(v => v.Group)
			.Select(g => new Group 
			{ 
				GroupId = g.Key, 
				Data = g.Select(e => e.Data).ToArray() 
			})
			.ToArray();

		private bool TryGetRootEntry(TData data, out Entry root)
		{
			root = null;

			if (!_datamap.TryGetValue(_bijectionMap.Invoke(data), out var entry))
				return false;

			root = entry.Root();
			return true;
		}


		public class Group
		{
			public TData[] Data { get; set; }
			public int GroupId { get; set; }
		}

		public class Entry
		{
			public TData Data { get; set; }

			public int Group { get; set; }

			public Entry Parent { get; set; }

			public List<Entry> Children { get; private set; } = new List<Entry>();

			public Entry()
			{
				Parent = this;
			}

			public Entry Root()
			{
				var tmp = this;
				while (tmp.Parent != tmp)
					tmp = tmp.Parent;

				return tmp;
			}

			public void Flatten()
			{
				var descendants = Descendants(true);

				var newChildren = new List<Entry>();
				foreach(var child in descendants)
				{
					child.Parent = this;
					child.Group = Group;

					child.Children.Clear();

					newChildren.Add(child);
				}

				Children = newChildren;
			}

			private IEnumerable<Entry> Descendants(bool isRoot = false)
			{
				IEnumerable<Entry> descendants = isRoot? new Entry[0] : new[] { this };
				foreach (var child in Children)
					descendants = descendants.Concat(child.Descendants());

				return descendants;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.Helpers.DataStructures
{
	public class Heap<V> where V : IComparable<V>
	{
		private readonly List<V> _list = new List<V>();
		private bool _isMaxHeap = true;

		private Heap(bool isMaxHeap)
		{
			_isMaxHeap = isMaxHeap;
		}

		private bool IsEven(int index) => index % 2 == 0;

		private int ParentIndexOf(int index) => index == 0 ? -1 : (index - (IsEven(index) ? 2 : 1)) / 2;

		private int LeftChildIndexOf(int index) => (index * 2) + 1;

		private int RightChildIndexOf(int index) => (index * 2) + 2;

		private bool TryGetLeftChildIndexOf(int parentIndex, out int leftChildIndex)
		{
			leftChildIndex = LeftChildIndexOf(parentIndex);

			if (leftChildIndex < _list.Count)
				return true;

			else
			{
				leftChildIndex = -1;
				return false;
			}
		}

		private bool TryGetRightChildIndexOf(int parentIndex, out int rightChildIndex)
		{
			rightChildIndex = RightChildIndexOf(parentIndex);

			if (rightChildIndex < _list.Count)
				return true;

			else
			{
				rightChildIndex = -1;
				return false;
			}
		}

		private bool CanBubbleUp(int childIndex, out int parentIndex)
		{
			parentIndex = ParentIndexOf(childIndex);

			if (childIndex <= 0)
				return false;

			else
			{
				var comparison = _list[parentIndex].CompareTo(_list[childIndex]);

				if (comparison >= 0)
					return _isMaxHeap ? false : true;

				else
					return _isMaxHeap ? true : false;
			}
		}

		private bool CanBubbleDown(int parentIndex, out int childIndex)
		{
			childIndex = -1;
			bool hasRightChild = TryGetRightChildIndexOf(parentIndex, out var rightChildIndex),
				 hasLeftChild = TryGetLeftChildIndexOf(parentIndex, out var leftChildIndex);

			if (hasRightChild && hasLeftChild)
			{
				var comparison = _list[leftChildIndex].CompareTo(_list[rightChildIndex]);

				childIndex =
					(_isMaxHeap && comparison < 0) ? rightChildIndex :
					(!_isMaxHeap && comparison > 0) ? rightChildIndex :
					leftChildIndex;

				comparison = _list[parentIndex].CompareTo(_list[childIndex]);

				childIndex =
					(_isMaxHeap && comparison < 0) ? childIndex :
					(!_isMaxHeap && comparison > 0) ? childIndex :
					-1;
			}
			else if(hasLeftChild)
			{
				var comparison = _list[parentIndex].CompareTo(_list[leftChildIndex]);

				childIndex =
					(_isMaxHeap && comparison < 0) ? leftChildIndex :
					(!_isMaxHeap && comparison > 0) ? leftChildIndex :
					-1;
			}

			return childIndex != -1;
		}


		private int BubbleUp(int index)
		{
			if (index == 0)
				return 0;

			else
			{
				while(CanBubbleUp(index, out var parentIndex))
				{
					var temp = _list[parentIndex];
					_list[parentIndex] = _list[index];
					_list[index] = temp;

					index = parentIndex;
				}

				return index;
			}
		}

		private int BubbleDown(int parentIndex)
		{
			while (CanBubbleDown(parentIndex, out var childIndex))
			{
				var temp = _list[parentIndex];
				_list[parentIndex] = _list[childIndex];
				_list[childIndex] = temp;

				parentIndex = childIndex;
			}

			return parentIndex;
		}


		public bool TryPeek(out V value)
		{
			if(_list.Count > 0)
			{
				value = _list[0];
				return true;
			}
			else
			{
				value = default;
				return false;
			}

		}


		public V Poll() => Remove(0);

		public V Remove(int index)
		{
			V value = _list[index];

			var last = _list[_list.Count - 1];
			_list.RemoveAt(_list.Count - 1);
			_list[index] = last;

			BubbleDown(index);

			return value;
		}

		public int Add(V value)
		{
			var index = _list.Count;
			_list.Add(value);

			return BubbleUp(index);
		}

		public void AddRange(params V[] values) => AddRange(values.AsEnumerable());

		public void AddRange(IEnumerable<V> values)
		{
			foreach (var v in values)
				Add(v);
		}

		public override string ToString() => string.Join(", ", _list.Select(x => x.ToString()));


		public static Heap<V> NewMaxHeap() => new Heap<V>(true);

		public static Heap<V> NewMinHeap() => new Heap<V>(false);
	}
}

using Codility01.Helpers.DataStructures;
using System;
using System.Diagnostics;
using Xunit;

namespace Codility01.Tests
{
	public class HeapTest
	{
		[Fact]
		public void MaxHeapTest1()
		{
			var heap = Heap<int>.NewMaxHeap();

			heap.AddRange(5, 2, 1, 0, 3, 4, 6, 3, 8, 6, 9, 9, 1, 21, 14, 56);
			Console.WriteLine(heap.ToString());
			Trace.WriteLine(heap.ToString());

			heap.Remove(6);

		}

		[Fact]
		public void MinHeapTest1()
		{
			var heap = Heap<int>.NewMinHeap();

			heap.AddRange(5, 2, 1, 0, 3, 4, 6, 3, 8, 6, 9, 9, 1, 21, 14, 56);
			Console.WriteLine(heap.ToString());
			Trace.WriteLine(heap.ToString());

			heap.Remove(6);

		}
	}
}

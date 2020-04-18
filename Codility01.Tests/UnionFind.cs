using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codility01.Tests
{
	public class UnionFind
	{
		[Fact]
		public void Test1()
		{
			var grid = new[]
			{
				new[] {'1', '1', '1', '1', '0' },
				new[] {'1', '1', '0', '1', '0' },
				new[] {'1', '1', '0', '0', '0' },
				new[] {'0', '0', '0', '0', '0' }
			};

			var islands = IslandCount.Challenge.Solve(grid);
			Assert.Equal(1, islands);
		}
	}
}

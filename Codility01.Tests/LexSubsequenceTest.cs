using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codility01.Tests
{
	public class LexSubsequenceTest
	{
		[Fact]
		public void Test()
		{
			var result = LexSubsequence.Challenge.SmallestSubsequence("ecbacba");
			Assert.Equal("adb", result);
		}
	}
}

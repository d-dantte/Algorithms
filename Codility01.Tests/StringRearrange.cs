using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Challenge = Codility01.StringRearrange.Challenge;

namespace Codility01.Tests
{
	public class StringRearrange
	{
		[Fact]
		public void Test1()
		{
			var result = Challenge.ReorganizeString("vvvlo");

			Assert.Equal("vlvov", result);
		}
		[Fact]
		public void Test2()
		{
			var result = Challenge.ReorganizeString("abbabbaaab");

		}
		[Fact]
		public void Test3()
		{
			var result = Challenge.ReorganizeString("ogccckcwmbmxtsbmozli");

		}
	}
}

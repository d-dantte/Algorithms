using Codility01.Helpers.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Kvp = System.Collections.Generic.KeyValuePair<char, string>;

namespace Codility01.Tests
{
	public class LCPTableTest
	{
		[Fact]
		public void FirstTest()
		{
			var stringPair = new Kvp('#', "AZAZA");
			var lcptable = new LongestCommonPrefixTable(new[] { stringPair });

			Assert.Equal(9, lcptable.UniqueSubstringCount());

			Console.WriteLine(lcptable);
		}

		[Fact]
		public void SecondTest()
		{
			var lcptable = new LongestCommonPrefixTable(new[]
			{
				new Kvp('*', "abca"),
				new Kvp('!', "bcad"),
				new Kvp('@', "daca")
			});

			var l = lcptable.LongestCommonKSubstringLength(2);
			Assert.Equal(3, l);
		}

		[Fact]
		public void ThirdTest()
		{
			var lcptable = new LongestCommonPrefixTable(new[]
			{
				new Kvp('*', "abcavddcabbddacdeaffggffasdzddweazs"),
				new Kvp('!', "szaewddzdsaffggffaedcaddbbacddvacba")
			});

			var l = lcptable.LongestPalindrome();
			Assert.Equal("affggffa", l);

			var all = lcptable.AllUniquePalindromes(1);
		}
	}
}

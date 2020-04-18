using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codility01.Tests
{
	public class WordSearchTests
	{
		[Fact]
		public void Test1()
		{
			var finder = new WordSearch.WordDictionary();
			finder.AddWord("a");
			finder.AddWord("ab");
			var r = finder.Search("a");
			r = finder.Search("a.");
			r = finder.Search("ab");
			r = finder.Search(".a");

			Assert.False(r);
		}
	}
}

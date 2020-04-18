using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Codility01.Tests
{
	public class MobSquadChallenges
	{
		[Fact]
		public void Challenge1()
		{

		}

		[Fact]
		public void Challenge2()
		{
			maximumContainers(new List<string> { "6 2 2" });
		}

		[Fact]
		public void Challenge3()
		{

		}

		public static string featuredProduct(List<string> products)
		{
			if (products == null || products.Count == 0)
				return null;

			var featured = products
				.GroupBy(p => p)
				.Select(g => new KeyValuePair<int, string>(g.Count(), g.First()))
				.OrderBy(kvp => kvp.Key)
				.ThenBy(kvp => kvp.Value)
				.LastOrDefault();

			return featured.Value;
		}

		public static void maximumContainers(List<string> scenarios)
		{
			scenarios.ForEach(Evaluate);
		}

		private static void Evaluate(string scenario)
		{
			var values = scenario.Split(new[] { ' ' });
			var budget = int.Parse(values[0]);
			var cost = int.Parse(values[1]);
			var unitReturn = int.Parse(values[2]);

			int received = budget/cost;
			int containers = received;
			while(containers > 0)
			{
				containers = Math.DivRem(containers, unitReturn, out var balanceContainer);
				received += containers;

				if (containers != 0)
					containers += balanceContainer;
			}

			Console.WriteLine(received);
		}
	}
}

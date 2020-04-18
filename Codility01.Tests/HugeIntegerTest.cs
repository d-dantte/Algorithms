using Codility01.Helpers;
using System;
using System.Numerics;
using Xunit;

namespace Codility01.Tests
{
	public class HugeIntegerTest
	{
		[Fact]
		public void Test1()
		{
			var hint = new HugeInteger("5");
			var hint2 = new HugeInteger("-5");

			var r = hint.ToString();
			Console.WriteLine(r);

			r = hint2.ToString();
			Console.WriteLine(r);

			var hint3 = hint + hint2;
			r = hint3.ToString();
			Console.WriteLine(r);

			var intv = 65464643;
			var intv2 = 54645646464;
			var intv3 = intv + intv2;
			var intv4 = intv3 * 17;

			hint = intv;
			hint2 = intv2;
			hint3 = hint + hint2;
			var hint4 = hint3 * 17;

			Assert.Equal(intv3.ToString(), hint3.ToString());
			Assert.Equal(intv4.ToString(), hint4.ToString());

			hint = "6546467575467543567546754356786546786545678654356786543";
			hint2 = "2312345654457653432406755434793810849473931759308439374375893392850934937594794890285089275938404644983879387239";
			hint4 = hint + hint2;
			Console.WriteLine(hint4);

			hint4 = hint * hint2;
			Console.WriteLine(hint4);

			hint4 = hint2 - hint2;
			Console.WriteLine(hint4);

			hint = "14";
			hint2 = "221";
			hint3 = hint / hint2;
			hint4 = hint2 / hint;
			Console.WriteLine(hint3);
			Console.WriteLine(hint4);

			hint3 = hint % hint2;
			hint4 = hint2 % hint;
			Console.WriteLine(hint3);
			Console.WriteLine(hint4);

			hint = "654";
			hint2 = "11205675467544";
			hint3 = hint / hint2;
			hint4 = hint2 / hint;
			Console.WriteLine(hint3);
			Console.WriteLine(hint4);

			hint = "10";
			hint3 = hint.Power(2);
			Console.WriteLine(hint3);

			hint = long.MaxValue;
			hint3 = hint.Power(20);
			Console.WriteLine(hint3);
		}

		[Fact]
		public void Test2()
		{
			BigInteger bint = BigInteger.Parse("4535675435678654567865435675432121231300000980000653432");
			BigInteger bint2 = BigInteger.Parse("23456098765");
			var bint3 = bint / bint2;
		}
	}
}

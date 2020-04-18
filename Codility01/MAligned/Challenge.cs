using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.MAligned
{
    class Challenge
    {
        public int Solution(int[] A, int M)
        {
            if (A.Length == 0)
                return 0;

            var distanceMap = new Dictionary<Pair, int>();

            for (int cnt = 0; cnt < A.Length; cnt++)
            {
                var anchor = A[cnt];

                for (int cnt2 = cnt + 1; cnt2 < A.Length; cnt2++)
                {
                    var test = A[cnt2];
                    distanceMap[new Pair(cnt, cnt2)] = Math.Abs(anchor - test);
                }
            }

            if (distanceMap.Count == 0)
                return 1;

            return distanceMap
                .Where(map => IsDivisibleBy(map.Value, M))
                .Count();
        }


        public static bool IsDivisibleBy(int numerator, int denominator) => numerator % denominator == 0;
    }

    internal class Pair
    {
        public int A { get; set; }
        public int B { get; set; }

        public Pair()
        { }

        public Pair(int a, int b)
        {
            A = a;
            B = b;
        }

        public override bool Equals(object obj)
        {
            return obj is Pair x
                && x.A == A
                && x.B == B;
        }

        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() => $"{A},{B}";
    }
}

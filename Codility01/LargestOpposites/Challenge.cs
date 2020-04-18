using System;
using System.Collections.Generic;

namespace Codility01.LargestOpposites
{
    public class Challenge
    {
        public int solution(int[] A)
        {
            var pairs = new Dictionary<int, int?>();
            int max = 0;
            for (int cnt = 0; cnt < A.Length; cnt++)
            {
                var number = A[cnt];
                var opposite = Opposite(number);

                if (pairs.ContainsKey(number))
                    continue;

                else if (pairs.ContainsKey(opposite))
                {
                    pairs[opposite] = number;

                    var absoluteNumber = Math.Abs(number);

                    if (max < absoluteNumber)
                        max = absoluteNumber;
                }

                else pairs[number] = null;
            }

            return max;
        }

        private int Opposite(int i) => -i;
    }
}

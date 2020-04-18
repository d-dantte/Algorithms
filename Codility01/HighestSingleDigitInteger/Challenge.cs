using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.HighestSingleDigitInteger
{
    public class Challenge
    {
        public int solution(int[] A)
        {
            return A
                .Where(a => a > -10 && a < 10)
                .Max();
        }
    }
}

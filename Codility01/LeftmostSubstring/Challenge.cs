using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.LeftmostSubstring
{
    public class Challenge
    {
        public int Solution(int A, int B)
        {
            if (A > B)
                return -1;

            var stringA = A.ToString();
            var stringB = B.ToString();

            return stringB.IndexOf(stringA);
        }
    }
}

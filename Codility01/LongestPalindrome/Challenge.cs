using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.LongestPalindrome
{
	public class Challenge
	{
        public static string LongestPalindrome(string s)
        {
            if (s.Length <= 1)
                return s;

            else
            {
                for(int subLength = s.Length; subLength > 0; subLength--)
                {
                    var maxStart = s.Length - subLength;
                    for (int start = 0; start <= maxStart; start++)
                    {
                        var subString = s.Substring(start, subLength);
                        if (IsPalindrome(subString))
                            return subString;
                    }
                }

                return "";
            }
        }

        private static bool IsPalindrome(string s)
        {
            return s.Equals(new string(s.Reverse().ToArray()));
        }
    }
}

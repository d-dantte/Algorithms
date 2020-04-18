using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.IntParser
{
	class Challenge
	{
        public static int MyAtoi(string str)
        {
            bool foundSign = false, foundDigit = false, isNegative = false;

            var chars = new List<char>();
            foreach (var @char in str)
            {
                if (!char.IsDigit(@char))
                {
                    if (!foundDigit && !foundSign)
                    {
                        if (!foundSign && (@char == '+' || @char == '-'))
                        {
                            foundSign = true;
                            isNegative = @char == '-';
                        }
                        else if (@char == ' ')
                            continue;

                        else break;
                    }

                    else break;
                }
                else
                {
                    chars.Add(@char);
                    foundDigit = true;
                }
            }

            var stringResult = string.Join("", chars);

            if (stringResult == string.Empty)
                return 0;

            else if (isNegative && IntCompare(stringResult, int.MinValue.ToString().Substring(1)) > 0)
                return int.MinValue;

            else if (!isNegative && IntCompare(stringResult, int.MaxValue.ToString()) > 0)
                return int.MaxValue;

            else return int.Parse((isNegative ? "-" : "") + stringResult);
        }

        private static int IntCompare(string first, string second)
        {
            var max = Math.Max(first.Length, second.Length);

            for (int cnt = max; cnt > 0; cnt--)
            {
                var fvalue = cnt > first.Length ? '0' : first[first.Length-cnt];
                var svalue = cnt > second.Length ? '0' : second[second.Length - cnt];
                if (fvalue > svalue)
                    return 1;

                else if (fvalue < svalue)
                    return -1;
            }

            return 0;
        }
    }
}

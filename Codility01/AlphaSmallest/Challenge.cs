using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.AlphaSmallest
{
    public class Challenge
    {
        public string Solution(string S)
        {
            var cache = new List<string>(S.Length);
            char? prev = null;

            for (int cnt = 0; cnt < S.Length; cnt++)
            {
                char current = S[cnt];

                if (prev != null && current < prev)
                {
                    cache.Add(new StringBuilder()
                        .Append(S.Substring(0, cnt - 1))
                        .Append(S.Substring(cnt))
                        .ToString());
                }

                prev = current;
            }

            if (cache.Count == 0) //all chars are in ascending or equal order
                cache.Add(S.Substring(0, S.Length - 1));

            return cache.OrderBy(s => s).First();
        }

        public string Solution2(string S)
        {
            var cache = new List<Info>(S.Length);
            char? prev = null;

            for (int cnt = 0; cnt < S.Length; cnt++)
            {
                char current = S[cnt];

                if (prev != null && current < prev)
                    cache.Add(new Info { Char = prev.Value, Index = cnt - 1 });

                prev = current;
            }

            if (cache.Count == 0) //all chars are in ascending or equal order
                return (S.Substring(0, S.Length - 1));

            else
            {
                var finalInfo = cache
                    .OrderByDescending(info => info.Char)
                    .ThenBy(info => info.Index)
                    .First();

                //splice
                if (finalInfo.Index == 0)
                    return S.Substring(1);

                else
                    return new StringBuilder()
                        .Append(S.Substring(0, finalInfo.Index))
                        .Append(S.Substring(finalInfo.Index + 1))
                        .ToString();
            }
        }

        internal class Info
        {
            public char Char { get; set; }
            public int Index { get; set; }
        }


        public static void Test1()
        {
            var result = new Challenge().Solution2("codility");
            Console.WriteLine($"{typeof(Challenge).FullName} Test1: {result}");
        }


        public static void Test2()
        {
            var result = new Challenge().Solution2("acb");
            Console.WriteLine($"{typeof(Challenge).FullName} Test2: {result}");
        }


        public static void Test3()
        {
            var result = new Challenge().Solution2("aaaa");
            Console.WriteLine($"{typeof(Challenge).FullName} Test3: {result}");
        }


        public static void Test4()
        {
            var result = new Challenge().Solution2("codolity");
            Console.WriteLine($"{typeof(Challenge).FullName} Test1: {result}");
        }
    }
}

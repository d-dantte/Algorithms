using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.StringRearrange
{
    public class Challenge
    {
        public static string ReorganizeString(string S)
        {
            if (string.IsNullOrEmpty(S))
                return "";

            //group letters
            var group = S
                .GroupBy(s => s)
                .ToDictionary(g => (char?)g.Key, g => g.Count());

            var newstring = new char[S.Length];
            var kindx = 0;
            var keys = group
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .ToArray();

            for (int cnt = 0; cnt < S.Length; cnt += 2)
            {
                newstring[cnt] = keys[kindx].Value;

                group[keys[kindx]]--;
                if (group[keys[kindx]] == 0)
                    kindx++;
            }

            for (int cnt = 1; cnt < S.Length; cnt += 2)
            {
                newstring[cnt] = keys[kindx].Value;

                group[keys[kindx]]--;
                if (group[keys[kindx]] == 0)
                    kindx++;
            }

            return new string(newstring);
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Codility01.LexSubsequence
{
    public class Challenge
    {
        public static string SmallestSubsequence(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            int pindex = 0;
            var charinfo = text
                .Distinct()
                .OrderBy(c => c)
                .ToDictionary(c => c, c => new Info { Index = -1, Preferred = pindex++ });

            var list = new List<char>(charinfo.Count);
            var perfect = new string(charinfo.Keys.OrderBy(c => c).ToArray());

            foreach (var c in text)
            {
                if (list.Count > 0 && charinfo[c].Index >= 0 && HasUnorderedPrevious(c, charinfo))
                {
                    list.RemoveAt(charinfo[c].Index);
                    var l = charinfo[c].Index;
                    foreach (var k in charinfo.Keys)
                    {
                        if (charinfo[k].Index > l)
                            charinfo[k].Index--;
                    }
                }
                else if (charinfo[c].Index >= 0)
                    continue;

                list.Add(c);
                charinfo[c].Index = list.Count - 1;

                if (charinfo.All(kvp => kvp.Value.Index >= 0)
                   && perfect.Equals(new string(list.ToArray())))
                    break;
            }

            return new string(list.ToArray());
        }

        public static bool HasUnorderedPrevious(char c, Dictionary<char, Info> info)
        {
            var l = info[c].Index;
            foreach(var kvp in info.OrderBy(ii => ii.Value.Preferred))
            {
                if (kvp.Key == c)
                    break;

                if (kvp.Value.Index > l)
                    return true;
            }
            return false;
        }
    }

    public class Info
    {
        public int Index { get; set; }
        public int Preferred { get; set; }

        public bool IsSettled => Index >= Preferred;

        public override string ToString() => $"I: {Index}, P: {Preferred}";
    }
}

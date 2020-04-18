using System.Collections.Generic;
using System.Linq;

namespace Codility01.WordLadder
{
    public class Challenge
    {
        public static int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {

            if (string.IsNullOrWhiteSpace(beginWord) || string.IsNullOrWhiteSpace(endWord))
                return 0;

            if (beginWord.Equals(endWord))
                return 0;

            var steps = 1;
            var found = false;
            var groups = wordList
                .Select(candidate =>
                {
                    if (endWord.Equals(candidate))
                        found = true;

                    return new KeyValuePair<int, string>(
                        Difference(beginWord, candidate),
                        candidate);
                })
                .Where(info => info.Key > 0)
                .GroupBy(info => info.Key)
                .ToDictionary(group => group.Key, info => info.ToList());

            if (found == false)
                return 0;

            IEnumerable<KeyValuePair<int, string>> prevs = new[] { new KeyValuePair<int, string>(0, beginWord) };
            foreach (var group in groups.OrderBy(g => g.Key))
            {
                if (steps == 1 && group.Key != 1)
                    break;

                //find everything in group that is one step away from the things in prev            
                steps++;
                if (TryFindNext(prevs, group.Value, endWord, out prevs))
                    return steps;

                if (prevs.Count() == 0)
                    break;
            }

            return 0;
        }

        public static bool TryFindNext(
            IEnumerable<KeyValuePair<int, string>> prev,
            IEnumerable<KeyValuePair<int, string>> current,
            string word,
            out IEnumerable<KeyValuePair<int, string>> result)
        {
            var next = new HashSet<KeyValuePair<int, string>>();
            foreach (var c in current)
            {
                foreach (var p in prev)
                {
                    if (word.Equals(c.Value))
                    {
                        result = next;
                        return true;
                    }

                    else if (Difference(p.Value, c.Value) == 1)
                        next.Add(c);
                }
            }

            result = next;
            return false;
        }

        public static int Difference(string word, string candidate)
        {
            int difference = 0;
            for (int cnt = 0; cnt < word.Length; cnt++)
            {
                if (word[cnt] != candidate[cnt])
                    difference++;
            }

            return difference;
        }
    }
}

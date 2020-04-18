using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.WordSearch
{
    public class WordDictionary
    {

        private readonly Dictionary<int, HashSet<string>> _data = new Dictionary<int, HashSet<string>>();
        private readonly Dictionary<string, bool> _searchResults = new Dictionary<string, bool>();

        /** Initialize your data structure here. */
        public WordDictionary()
        {

        }

        /** Adds a word into the data structure. */
        public void AddWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                return;

            if (!_data.ContainsKey(word.Length))
                _data[word.Length] = new HashSet<string>();

            if (_data[word.Length].Add(word))
            {
                foreach (var kvp in _searchResults.ToArray())
                {
                    if (!kvp.Value && kvp.Key.Length == word.Length)
                        _searchResults.Remove(kvp.Key);
                }
            }
        }

        /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
        public bool Search(string searchWord)
        {

            if (string.IsNullOrEmpty(searchWord))
                return false;

            if (searchWord.Contains("."))
            {
                if (_searchResults.ContainsKey(searchWord))
                    return _searchResults[searchWord];

                if (!_data.ContainsKey(searchWord.Length))
                    return false;

                var regex = new System.Text.RegularExpressions.Regex($"^{searchWord}$");
                outer:  foreach (var word in _data[searchWord.Length])
                {
                    if (regex.IsMatch(searchWord))
                        return _searchResults[searchWord] = true;
                }

                return _searchResults[searchWord] = false;
            }

            return _data.ContainsKey(searchWord.Length)
                && _data[searchWord.Length].Contains(searchWord);
        }
    }
}

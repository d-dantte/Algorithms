using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility01.Helpers.DataStructures
{
	public class LongestCommonPrefixTable
	{
		private readonly List<Entry> _entryList = new List<Entry>();
		private Dictionary<char, string> _originalStrings;
		private string _concatenated;

		public LongestCommonPrefixTable(IEnumerable<KeyValuePair<char, string>> strings)
		{
			_originalStrings = strings.ToDictionary(
				pair => pair.Key,
				pair => pair.Value);

			var sbuilder = new StringBuilder();
			foreach(var pair in strings)
			{
				sbuilder
					.Append(pair.Value)
					.Append(pair.Key);
			}
			_concatenated = sbuilder.ToString();

			BuildEntryList();
		}

		private void BuildEntryList()
		{
			Enumerable
				.Range(0, _concatenated.Length)
				.Select(index => new Entry
				{
					SuffixIndex = index,
					Group = EvaluateGroup(index),
					SuffixString = _concatenated.Substring(index)

				})
				.Where(entry => !_originalStrings.ContainsKey(_concatenated[entry.SuffixIndex]))
				.OrderBy(entry => entry.Suffix(_concatenated))
				.Aggregate((Entry)null, (prev, current) =>
				{
					if(prev == null)
						current.LCPC = 0;

					else
					{
						var lim = Math.Min(
							_concatenated.Length - prev.SuffixIndex,
							_concatenated.Length - current.SuffixIndex);
						for (int cnt = 0; cnt < lim; cnt++)
						{
							if (_concatenated[prev.SuffixIndex + cnt] == _concatenated[current.SuffixIndex + cnt])
								current.LCPC++;

							else break;
						}
					}

					_entryList.Add(current);
					return current;
				});
		}

		private int EvaluateGroup(int index)
		{
			int group = 1;
			var length = 0;
			foreach(var @string in _originalStrings.Values)
			{
				length += @string.Length;
				if (index < length + group)
					return group;

				group++;
			}
			//should never happen
			return -1;
		}


		public int UniqueSubstringCount()
		{
			var n = _originalStrings.Sum(x => x.Value.Length);
			return ((n * (n + 1)) / 2) - _entryList.Sum(e => e.LCPC);
		}

		public override string ToString() => string.Join(Environment.NewLine, _entryList.Select(e => e.ToString(this)));

		public int LongestCommonKSubstringLength(int k = 2)
		{
			if (k < 2 || k > _originalStrings.Count)
				return -1;

			int wstart = 0;
			int wcount = 1;
			int lcsl = 0;
			var groupTable = _entryList
				.GroupBy(e => e.Group)
				.ToDictionary(e => e.Key, e => e.Key == _entryList[0].Group ? 1 : 0);

			while (wstart + wcount < _entryList.Count)
			{
				var temp = 0;

				//if we have less than the requisite number of groups represented in our window
				if (groupTable.Values.Count(e => e != 0) < k)
				{
					if (TryGetAt(wstart + wcount, out var e))
						groupTable[e.Group]++;

					wcount++;
				}
				else
				{
					if ((temp = _entryList.Skip(wstart + 1).Take(wcount - 1).Min(en => en.LCPC)) > lcsl)
						lcsl = temp;

					if(TryGetAt(wstart, out var e))
						groupTable[e.Group]--;

					wstart++;
					wcount--;
				}
			}

			return lcsl;
		}

		public string LongestPalindrome()
		{
			return _entryList
				.Where(e => e.LCPC > 1)
				.Where(e => e.IsPalindrome(_concatenated))
				.OrderByDescending(e => e.LCPC)
				.Select(e => e.CommonSubstring(_concatenated))
				.FirstOrDefault();
		}

		public string[] AllUniquePalindromes(int minLength = 2)
		{
			var palindromes = _entryList
				.Where(e => e.LCPC >= minLength)
				.Where(e => e.IsPalindrome(_concatenated))
				.Select(e => e.CommonSubstring(_concatenated));
			var set = new HashSet<string>();

			foreach (var p in palindromes)
				set.Add(p);

			return set.ToArray();
		}

		private bool TryGetAt(int index, out Entry e)
		{
			if(index >= 0 && index < _entryList.Count)
			{
				e = _entryList[index];
				return true;
			}
			else
			{
				e = null;
				return false;
			}
		}


		public class Entry
		{
			public int SuffixIndex { get; set; }

			public string SuffixString { get; set; } /////

			public string Suffix(string @string) => @string.Substring(SuffixIndex);

			public string CommonSubstring(string @string) => @string.Substring(SuffixIndex, LCPC);

			public bool IsPalindrome(string @string)
			{
				var sub = @string.Substring(SuffixIndex, LCPC);

				char[] charArray = sub.ToCharArray();
				Array.Reverse(charArray);
				var reverseSub = new string(charArray);

				return sub.Equals(reverseSub, StringComparison.InvariantCulture);
			}

			public int LCPC { get; set; }
			public int LongestCommonPrefixCount => LCPC;

			public int Group { get; set; }

			public override string ToString() => $"{LCPC}[ {Group} ]/ {SuffixString}";

			public string ToString(LongestCommonPrefixTable table) 
				=> $"{LCPC}[{Group} | {Suffix(table._concatenated)}]";
		}
	}
}

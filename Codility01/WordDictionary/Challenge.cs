using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility01.WordDictionary
{
	public class WordNode
	{
		public List<WordNode> Children { get; } = new List<WordNode>();

		public string Word { get; }

		public int SenenceIndex { get; }

		public bool IsRecognizedWord { get; }

		public WordNode Parent { get; }

		public IEnumerable<WordNode> Sentence()
		{
			var list = new List<WordNode>();
			var node = this;
			while(node != null)
			{
				list.Add(node);
				node = node.Parent;
			}

			list.Reverse();
			return list;
		}

		public string SentenceString()
		{
			var words = Sentence()
				.Select(node => node.Word)
				.ToArray();

			return string.Join(" ", words);
		}

		public int UnrecognizedNodeCount()
		{
			return Sentence().Count(node => !node.IsRecognizedWord);
		}


		private WordNode(string word, int index, bool isrecognized, WordNode parent)
		{
			SenenceIndex = index;
			IsRecognizedWord = isrecognized;
			Parent = parent;
			Word = string.IsNullOrWhiteSpace(word) ? throw new Exception()
				: IsRecognizedWord ? word.ToLower()
				: word.ToUpper();
		}

		public static WordNode UnrecognizedWord(string word, int index, WordNode parent = null)
		{
			return new WordNode(word, index, false, parent);
		}

		public static WordNode RecognizedWord(string word, int index, WordNode parent = null)
		{
			return new WordNode(word, index, true, parent);
		}
	}

	public class Root
	{
		private List<WordNode> _nodes = new List<WordNode>();

		public IEnumerable<WordNode> SentenceTrees => _nodes;

		public Root(IEnumerable<WordNode> nodes)
		{
			_nodes.AddRange(nodes ?? throw new Exception());
		}

		public WordNode[] LeafNodes()
		{
			return _nodes
				.SelectMany(FindLeafNodes)
				.ToArray();
		}

		private static IEnumerable<WordNode> FindLeafNodes(WordNode node)
		{
			if (node.Children.Count == 0)
				return new[] { node };

			else
				return node.Children.SelectMany(FindLeafNodes);
		}

		public static IEnumerable<WordNode> BuildSentenceTree(
			string @string, 
			int index, 
			List<string> orderedDictionary,
			WordNode parent)
		{
			if (index == @string.Length)
				yield break;

			var found = false;
			for (int cnt = 0; cnt < orderedDictionary.Count; cnt++)
			{
				var dictionaryWord = orderedDictionary[cnt];
				if (IsMatch(@string, index, dictionaryWord))
				{
					found = true;
					var recognized = WordNode.RecognizedWord(
						@string.Substring(index, dictionaryWord.Length),
						index,
						parent);

					recognized.Children.AddRange(BuildSentenceTree(
						@string,
						index + dictionaryWord.Length,
						orderedDictionary,
						recognized));

					yield return recognized;
				}
			}

			if (found)
				yield break;

			//couldnt find any words from the dictionary, so find the next full unrecognizable word
			for (int cnt = index + 1; cnt < @string.Length; cnt++)
			{
				if(HasMatch(@string, cnt, orderedDictionary))
				{
					var unrecognized = WordNode.UnrecognizedWord(
						@string.Substring(index, cnt - index),
						index,
						parent);

					unrecognized.Children.AddRange(BuildSentenceTree(
						@string,
						index + unrecognized.Word.Length,
						orderedDictionary,
						unrecognized));

					yield return unrecognized;
					yield break;
				}
			}

			//couldn't find anything
			yield return WordNode.UnrecognizedWord(
				@string.Substring(index),
				index,
				parent);
		}

		private static bool IsMatch(string @string, int startIndex, string word)
		{
			if (word.Length > @string.Length - startIndex)
				return false;

			else return @string
				.Substring(startIndex, word.Length)
				.Equals(word, StringComparison.InvariantCultureIgnoreCase);
		}

		private static bool HasMatch(string @string, int startIndex, List<string> orderedWords)
		{
			foreach(var word in orderedWords)
			{
				if (word.Length > @string.Length - startIndex)
					return false;

				else if (IsMatch(@string, startIndex, word))
					return true;
			}
			return false;
		}
	}

	class Challenge
	{
		public static void FindString(string sentence, List<string> dictionary)
		{
			var orderedDictionary = dictionary
				.OrderBy(word => word.Length)
				.ToList();

			var sentenceRoot = new Root(Root.BuildSentenceTree(
				sentence,
				0,
				orderedDictionary,
				null));


			//find the sentence with least unrecognized words
			var leafNodes = sentenceRoot.LeafNodes();
			var leastRecognized = leafNodes
				.OrderBy(node => node.UnrecognizedNodeCount())
				.First();


			Console.WriteLine(leastRecognized.SentenceString());
		}
	}
}

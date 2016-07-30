using System;
using System.Collections.Generic;
using System.Linq;

namespace GitUI.CommandsDialogs.CommitDialog
{
    public class WordWrapper
    {
        public static string Wrap(string text, int lineLimit)
        {
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var newLines = new List<string>();
            foreach (var line in lines)
            {
                newLines.AddRange(InternalWrapSingleLine(line, lineLimit));
            }
            return String.Join(Environment.NewLine, newLines);
        }

        public static string WrapSingleLine(string text, int lineLimit)
        {
            var lines = InternalWrapSingleLine(text, lineLimit);
            return String.Join(Environment.NewLine, lines);
        }

        private static IEnumerable<string> InternalWrapSingleLine(string line, int lineLimit)
        {
            var wrapper = new WrapperState(lineLimit);
            foreach (var word in line.Split())
            {
                if (!wrapper.CanAddWord(word))
                {
                    yield return wrapper.GetLineAndReset();
                }
                wrapper.AddWord(word);
            }
            if (wrapper.HasWords)
            {
                yield return wrapper.GetLineAndReset();
            }
        }

        private class WrapperState
        {
            private readonly int lineLimit;
            private List<string> wordList = new List<string>();
            private int wordsLength;

            public WrapperState(int lineLimit)
            {
                this.lineLimit = lineLimit;
                Reset();
            }

            public bool HasWords { get; set; }

            public void AddWord(string word)
            {
                wordList.Add(word);
                wordsLength += word.Length;
                HasWords = true;
            }

            public bool CanAddWord(string word)
            {
                var newLength = wordsLength + wordList.Count + word.Length;
                return (newLength < lineLimit) || wordList.Any() == false;
            }

            public string GetLineAndReset()
            {
                var line = String.Join(" ", wordList);
                Reset();
                return line;
            }

            private void Reset()
            {
                wordList.Clear();
                wordsLength = 0;
                HasWords = false;
            }
        }
    }
}

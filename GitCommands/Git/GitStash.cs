using System;

namespace GitCommands
{
    /// <summary>Stored local modifications.</summary>
    public class GitStash
    {
        private const string CustomFormat = "On ";

        private const string DefaultFormat = "WIP on ";

        /// <summary>"stash@{i}"</summary>
        private const string NameFormat = "stash@{{{0}}}";

        private static int CustomFormatLength = CustomFormat.Length;

        private static int DefaultFormatLength = DefaultFormat.Length;

        private readonly string _stash;

        /// <summary>Initializes a new <see cref="GitStash"/> with all properties null.</summary>
        public GitStash() { }

        public GitStash(string stash, int i)
        {
            if (string.IsNullOrWhiteSpace(stash))
            {
                throw new ArgumentException("Stash has NO characters.", "stash");
            }

            // "stash@{i}: WIP on {branch}: {PreviousCommitMiniSHA} {PreviousCommitMessage}"
            // "stash@{i}: On {branch}: {Message}"

            _stash = stash;
            Index = i;

            Name = string.Format(NameFormat, Index);

            int msgStart = stash.IndexOf(':') + 1;
            if (msgStart < stash.Length)
            {
                Message = stash.Substring(msgStart).Trim();
                FindBranch();
            }
        }

        /// <summary>Name of the branch that was current when the stash was made.</summary>
        public string Branch { get; set; }

        /// <summary>Gets the index of the stash in the list.</summary>
        public int Index { get; set; }

        /// <summary>Short description of the commit the stash was based on.</summary>
        public string Message { get; set; }

        /// <summary>Name of the stash. <remarks>Usually, "stash@{n}"</remarks></summary>
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (null == obj) { return false; }
            if (this == obj) { return true; }

            GitStash other = obj as GitStash;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return (_stash.GetHashCode());
        }

        public override string ToString()
        {
            return Name;
        }

        protected bool Equals(GitStash other)
        {
            return string.Equals(_stash, other._stash);
        }

        private void FindBranch()
        {
            int trimLength = Message.StartsWith(DefaultFormat)
                ? DefaultFormatLength // "WIP on "
                : CustomFormatLength;//  "On "
            var branchStart = Message.Remove(0, trimLength);// "{branch}: {SHA} {msg}"
            Branch = branchStart.Substring(0, branchStart.IndexOf(':'));
        }
    }
}

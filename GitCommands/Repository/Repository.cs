using System;
using System.Xml.Serialization;

namespace GitCommands.Repository
{
    public class Repository
    {
        private string _path;

        public Repository()
        {
            Anchor = RepositoryAnchor.None;
        }

        public Repository(string path, string description, string title)
            : this()
        {
            Path = path;
            Description = description;
            Title = title;
            RepositoryType = RepositoryType.Repository;
        }

        public enum RepositoryAnchor
        {
            MostRecent,
            LessRecent,
            None
        }

        public RepositoryAnchor Anchor { get; set; }
        public string Description { get; set; }

        [XmlIgnore]
        public bool IsRemote
        {
            get { return PathIsUrl(Path); }
        }

        public string Path
        {
            get
            {
                return _path ?? string.Empty;
            }
            set
            {
                _path = value;
            }
        }

        [XmlIgnore]
        public RepositoryType RepositoryType { get; set; }

        public string Title { get; set; }

        public static bool PathIsUrl(string path)
        {
            return !String.IsNullOrEmpty(path) &&
                (path.StartsWith("http", StringComparison.CurrentCultureIgnoreCase) ||
                 path.StartsWith("git", StringComparison.CurrentCultureIgnoreCase) ||
                 path.StartsWith("ssh", StringComparison.CurrentCultureIgnoreCase));
        }

        public void Assign(Repository source)
        {
            if (source == null)
                return;
            Path = source.Path;
            Title = source.Title;
            Description = source.Description;
            RepositoryType = source.RepositoryType;
        }

        public override string ToString()
        {
            return Path + " (" + Anchor.ToString() + ")";
        }
    }
}

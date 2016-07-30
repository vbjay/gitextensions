using System;
using System.Collections.Generic;

namespace FindLargeFiles
{
    public class GitObject
    {
        public GitObject(string sha, string path, int size, string commit)
        {
            SHA = sha;
            Path = path;
            sizeInBytes = size;
            compressedSizeInBytes = -1;
            Commit = new HashSet<string>();
            Commit.Add(commit);
        }

        public int CommitCount { get { return Commit.Count; } }
        public string CompressedSize { get { return compressedSizeInBytes >= 0 ? String.Format("{0:F2} Mb", compressedSizeInBytes / 1024.0f / 1024) : "<Unknown>"; } }
        public bool Delete { get; set; }
        public DateTime LastCommitDate { get; set; }
        public string Path { get; set; }
        public string SHA { get; set; }
        public string Size { get { return String.Format("{0:F2} Mb", sizeInBytes / 1024.0f / 1024); } }
        internal HashSet<string> Commit { get; set; }
        internal int compressedSizeInBytes { get; set; }
        internal int sizeInBytes { get; set; }
    }
}

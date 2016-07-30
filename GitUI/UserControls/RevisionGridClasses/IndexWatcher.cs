using System;
using System.IO;
using GitCommands;

namespace GitUI.UserControls.RevisionGridClasses
{
    public class IndexChangedEventArgs : EventArgs
    {
        public IndexChangedEventArgs(bool isIndexChanged)
        {
            IsIndexChanged = isIndexChanged;
        }

        public bool IsIndexChanged { get; private set; }
    }

    public sealed class IndexWatcher : IDisposable
    {
        private readonly IGitUICommandsSource UICommandsSource;

        private bool enabled;

        private bool indexChanged;

        private string Path;

        public IndexWatcher(IGitUICommandsSource aUICommandsSource)
        {
            UICommandsSource = aUICommandsSource;
            UICommandsSource.GitUICommandsChanged += UICommandsSource_GitUICommandsChanged;
            GitIndexWatcher = new FileSystemWatcher();
            RefsWatcher = new FileSystemWatcher();
            SetFileSystemWatcher();

            IndexChanged = true;
            GitIndexWatcher.Changed += fileSystemWatcher_Changed;
            RefsWatcher.Changed += fileSystemWatcher_Changed;
        }

        public event EventHandler<IndexChangedEventArgs> Changed;

        public bool IndexChanged
        {
            get
            {
                if (!enabled)
                    return true;

                if (Path != Module.GetGitDirectory())
                    return true;

                return indexChanged;
            }
            set
            {
                indexChanged = value;
                GitIndexWatcher.EnableRaisingEvents = !IndexChanged;

                if (Changed != null)
                    Changed(this, new IndexChangedEventArgs(IndexChanged));
            }
        }

        private FileSystemWatcher GitIndexWatcher { get; set; }

        private GitModule Module { get { return UICommands.Module; } }

        private FileSystemWatcher RefsWatcher { get; set; }

        private GitUICommands UICommands
        {
            get
            {
                return UICommandsSource.UICommands;
            }
        }

        public void Clear()
        {
            IndexChanged = true;
            RefreshWatcher();
        }

        public void Dispose()
        {
            enabled = false;
            GitIndexWatcher.EnableRaisingEvents = false;
            GitIndexWatcher.Changed -= fileSystemWatcher_Changed;
            RefsWatcher.Changed -= fileSystemWatcher_Changed;
            GitIndexWatcher.Dispose();
            RefsWatcher.Dispose();
        }

        public void Reset()
        {
            IndexChanged = false;
            RefreshWatcher();
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            IndexChanged = true;
        }

        private void RefreshWatcher()
        {
            if (Path != Module.GetGitDirectory() ||
                enabled != GitCommands.AppSettings.UseFastChecks)
                SetFileSystemWatcher();
        }

        private void SetFileSystemWatcher()
        {
            if (!Module.IsValidGitWorkingDir())
            {
                GitIndexWatcher.EnableRaisingEvents = false;
                RefsWatcher.EnableRaisingEvents = false;
            }
            else
            {
                try
                {
                    enabled = GitCommands.AppSettings.UseFastChecks;

                    Path = Module.GetGitDirectory();

                    GitIndexWatcher.Path = Path;
                    GitIndexWatcher.Filter = "index";
                    GitIndexWatcher.IncludeSubdirectories = false;
                    GitIndexWatcher.EnableRaisingEvents = enabled;

                    RefsWatcher.Path = System.IO.Path.Combine(Path, "refs");
                    RefsWatcher.IncludeSubdirectories = true;
                    RefsWatcher.EnableRaisingEvents = enabled;
                }
                catch
                {
                    enabled = false;
                }
            }
        }

        private void UICommandsSource_GitUICommandsChanged(object sender, GitUICommandsChangedEventArgs e)
        {
            Clear();
        }
    }
}

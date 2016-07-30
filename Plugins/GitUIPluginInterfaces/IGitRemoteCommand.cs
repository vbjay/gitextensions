using System;

namespace GitUIPluginInterfaces
{
    public delegate void GitRemoteCommandCompletedEventHandler(object sender, GitRemoteCommandCompletedEventArgs e);

    public interface IGitRemoteCommand
    {
        event GitRemoteCommandCompletedEventHandler Completed;

        string CommandOutput { get; }
        string CommandText { get; set; }
        bool ErrorOccurred { get; }
        object OwnerForm { get; set; }
        string Remote { get; set; }
        string Title { get; set; }

        void Execute();
    }

    public class GitRemoteCommandCompletedEventArgs : EventArgs
    {
        public GitRemoteCommandCompletedEventArgs(IGitRemoteCommand command, bool isError, bool handled)
        {
            Command = command;
            IsError = isError;
            Handled = handled;
        }

        public IGitRemoteCommand Command { get; private set; }

        public bool Handled { get; set; }
        public bool IsError { get; set; }
    }
}

using EnvDTE;
using EnvDTE80;
using GitPlugin.Git;

namespace GitPlugin.Commands
{
    public abstract class CommandBase
    {
        public bool RunForSelection { get; set; }

        abstract public bool IsEnabled(DTE2 application);

        abstract public void OnCommand(DTE2 application, OutputWindowPane pane);

        protected static void RunGitEx(string command, string filename)
        {
            GitCommands.RunGitEx(command, filename);
        }
    }
}

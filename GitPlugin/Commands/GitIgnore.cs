using EnvDTE;

namespace GitPlugin.Commands
{
    public sealed class GitIgnore : ItemCommandBase
    {
        protected override CommandTarget SupportedTargets
        {
            get { return CommandTarget.Any; }
        }

        protected override void OnExecute(SelectedItem item, string fileName, OutputWindowPane pane)
        {
            RunGitEx("gitignore", fileName);
        }
    }
}

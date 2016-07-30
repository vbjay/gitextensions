using EnvDTE;

namespace GitPlugin.Commands
{
    public sealed class Rebase : ItemCommandBase
    {
        protected override CommandTarget SupportedTargets
        {
            get { return CommandTarget.Any; }
        }

        protected override void OnExecute(SelectedItem item, string fileName, OutputWindowPane pane)
        {
            RunGitEx("rebase", fileName);
        }
    }
}

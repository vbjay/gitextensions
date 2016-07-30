using EnvDTE;

namespace GitPlugin.Commands
{
    public sealed class ViewChanges : ItemCommandBase
    {
        protected override CommandTarget SupportedTargets
        {
            get { return CommandTarget.SolutionExplorerFileItem; }
        }

        protected override void OnExecute(SelectedItem item, string fileName, OutputWindowPane pane)
        {
            RunGitEx("viewdiff", fileName);
        }
    }
}

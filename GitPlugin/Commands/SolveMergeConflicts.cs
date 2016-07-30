using EnvDTE;

namespace GitPlugin.Commands
{
    public sealed class SolveMergeConflicts : ItemCommandBase
    {
        protected override CommandTarget SupportedTargets
        {
            get { return CommandTarget.SolutionExplorerFileItem; }
        }

        protected override void OnExecute(SelectedItem item, string fileName, OutputWindowPane pane)
        {
            RunGitEx("mergeconflicts", fileName);
        }
    }
}

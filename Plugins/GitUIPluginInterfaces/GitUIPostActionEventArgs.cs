using System.Windows.Forms;

namespace GitUIPluginInterfaces
{
    public class GitUIPostActionEventArgs : GitUIBaseEventArgs
    {
        public GitUIPostActionEventArgs(IWin32Window ownerForm, IGitUICommands gitUICommands, bool actionDone)
            : base(ownerForm, gitUICommands)
        {
            ActionDone = actionDone;
        }

        public bool ActionDone { get; private set; }
    }
}

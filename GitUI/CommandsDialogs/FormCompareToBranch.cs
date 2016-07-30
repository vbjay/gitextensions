using System;
using System.Windows.Forms;

namespace GitUI.CommandsDialogs
{
    public partial class FormCompareToBranch : GitModuleForm
    {
        public FormCompareToBranch(GitUICommands aCommands, string selectedCommit) : base(aCommands)
        {
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            InitializeComponent();
            Translate();
            if (!IsUICommandsInitialized)
            {// UICommands is not initialized in translation unit test.
                return;
            }
            branchSelector.Initialize(remote: true, containRevisons: null);
            branchSelector.CommitToCompare = selectedCommit;
            Activated += OnActivated;
        }

        private FormCompareToBranch()
                    : this(null, null)
        {
        }

        public string BranchName { get; private set; }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (branchSelector.SelectedBranchName.IsNotNullOrWhitespace())
            {
                BranchName = branchSelector.SelectedBranchName;
                DialogResult = DialogResult.OK;
                Close();
            }
            branchSelector.Focus();
        }

        private void OnActivated(object sender, EventArgs eventArgs)
        {
            branchSelector.Focus();
        }
    }
}

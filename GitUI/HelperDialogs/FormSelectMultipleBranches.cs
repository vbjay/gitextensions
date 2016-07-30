using System;
using System.Collections.Generic;
using System.Linq;
using GitCommands;

namespace GitUI.HelperDialogs
{
    public partial class FormSelectMultipleBranches : GitExtensionsForm
    {
        public FormSelectMultipleBranches(IList<GitRef> branchesToSelect)
        {
            InitializeComponent();
            Translate();

            if (branchesToSelect.Count > 350)
                Branches.MultiColumn = true;

            Branches.DisplayMember = "Name";
            Branches.Items.AddRange(branchesToSelect.ToArray());
        }

        // only for translation
        private FormSelectMultipleBranches()
            : base(true)
        {
            InitializeComponent();
            Translate();
        }

        public IList<GitRef> GetSelectedBranches()
        {
            IList<GitRef> branches = new List<GitRef>();

            foreach (GitRef head in Branches.CheckedItems)
                branches.Add(head);

            return branches;
        }

        public void SelectBranch(string name)
        {
            int index = 0;
            foreach (object item in Branches.Items)
            {
                GitRef branch = item as GitRef;
                if (branch != null && branch.Name == name)
                {
                    Branches.SetItemChecked(index, true);
                    return;
                }
                index++;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

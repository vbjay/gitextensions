using System;

namespace GitUI.CommandsDialogs
{
    public sealed partial class FormAddFiles : GitModuleForm
    {
        public FormAddFiles(GitUICommands aCommands, string addFile)
                    : base(aCommands)
        {
            InitializeComponent();
            Translate();
            Filter.Text = addFile ?? ".";
        }

        public FormAddFiles(GitUICommands aCommands)
                    : this(aCommands, null)
        {
        }

        /// <summary>
        /// For VS designer
        /// </summary>
        private FormAddFiles()
            : this(null)
        {
        }

        private void AddFilesClick(object sender, EventArgs e)
        {
            if (FormProcess.ShowDialog(this, string.Format("add{0} \"{1}\"", force.Checked ? " -f" : "", Filter.Text), false))
                Close();
        }

        private void ShowFilesClick(object sender, EventArgs e)
        {
            FormProcess.ShowDialog(this, string.Format("add --dry-run{0} \"{1}\"", force.Checked ? " -f" : "", Filter.Text), false);
        }
    }
}

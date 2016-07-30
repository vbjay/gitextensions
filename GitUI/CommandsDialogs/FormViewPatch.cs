using System;
using System.Windows.Forms;
using PatchApply;
using ResourceManager;

namespace GitUI.CommandsDialogs
{
    public partial class FormViewPatch : GitModuleForm
    {
        private readonly TranslationString _patchFileFilterString =
            new TranslationString("Patch file (*.Patch)");

        private readonly TranslationString _patchFileFilterTitle =
            new TranslationString("Select patch file");

        public FormViewPatch(GitUICommands aCommands)
            : base(aCommands)
        {
            InitializeComponent(); Translate();

            PatchManager = new PatchManager();
        }

        public Patch CurrentPatch { get; set; }

        public PatchManager PatchManager { get; set; }

        public void LoadPatch(string patch)
        {
            PatchFileNameEdit.Text = patch;
            LoadButton_Click(null, null);
        }

        private void BrowsePatch_Click(object sender, EventArgs e)
        {
            PatchFileNameEdit.Text = SelectPatchFile(@".");
            LoadButton_Click(sender, e);
        }

        private void GridChangedFiles_SelectionChanged(object sender, EventArgs e)
        {
            if (GridChangedFiles.SelectedRows.Count == 0) return;

            var patch = (Patch)GridChangedFiles.SelectedRows[0].DataBoundItem;
            CurrentPatch = patch;

            if (patch == null) return;

            ChangesList.ViewPatch(patch);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                PatchManager.PatchFileName = PatchFileNameEdit.Text;
                PatchManager.LoadPatchFile(false, Module.FilesEncoding);

                GridChangedFiles.DataSource = PatchManager.Patches;
            }
            catch
            {
            }
        }

        private void PatchFileNameEdit_TextChanged(object sender, EventArgs e)
        {
        }

        private string SelectPatchFile(string initialDirectory)
        {
            using (var dialog = new OpenFileDialog
            {
                Filter = _patchFileFilterString.Text + "|*.Patch",
                InitialDirectory = initialDirectory,
                Title = _patchFileFilterTitle.Text
            })
            {
                return (dialog.ShowDialog(this) == DialogResult.OK) ? dialog.FileName : PatchFileNameEdit.Text;
            }
        }

        private void ViewPatch_Load(object sender, EventArgs e)
        {
        }
    }
}

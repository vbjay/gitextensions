using System;
using System.Windows.Forms;
using PatchApply;
using ResourceManager;

namespace GitUI
{
    public partial class PatchGrid : GitModuleControl
    {
        private readonly TranslationString _unableToShowPatchDetails = new TranslationString("Unable to show details of patch file.");

        public PatchGrid()
        {
            InitializeComponent(); Translate();
            Patches.CellPainting += Patches_CellPainting;
        }

        public void Initialize()
        {
            if (Module.InTheMiddleOfInteractiveRebase())
                Patches.DataSource = Module.GetInteractiveRebasePatchFiles();
            else
                Patches.DataSource = Module.GetRebasePatchFiles();
        }

        protected override void OnRuntimeLoad(EventArgs e)
        {
            Initialize();
        }

        private static void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private static void Patches_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        }

        private void Patches_DoubleClick(object sender, EventArgs e)
        {
            if (Patches.SelectedRows.Count != 1) return;

            var patchFile = (PatchFile)Patches.SelectedRows[0].DataBoundItem;

            if (string.IsNullOrEmpty(patchFile.FullName))
            {
                MessageBox.Show(_unableToShowPatchDetails.Text);
                return;
            }

            UICommands.StartViewPatchDialog(patchFile.FullName);
        }
    }
}

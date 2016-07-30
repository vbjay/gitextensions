using System;
using System.Windows.Forms;

namespace GitUI.CommandsDialogs.SettingsDialog
{
    public partial class SimpleHelpDisplayDialog : Form
    {
        public SimpleHelpDisplayDialog()
        {
            InitializeComponent();
        }

        public string ContentText { get; set; }
        public string DialogTitle { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Text = DialogTitle;
            textBox1.Text = ContentText;
            textBox1.Select(0, 0);
        }
    }
}

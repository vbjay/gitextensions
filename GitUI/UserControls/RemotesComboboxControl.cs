using System;

namespace GitUI.UserControls
{
    public partial class RemotesComboboxControl : GitModuleControl
    {
        private bool _allowMultiselect;

        public RemotesComboboxControl()
        {
            InitializeComponent();
            Translate();
            AllowMultiselect = false;
        }

        public bool AllowMultiselect
        {
            get { return _allowMultiselect; }
            set
            {
                _allowMultiselect = value;
                buttonSelectMultipleRemotes.Visible = _allowMultiselect;
                if (_allowMultiselect)
                {
                    throw new NotImplementedException();
                }
            }
        }

        public string SelectedRemote { get { return (string)comboBoxRemotes.Text; } set { comboBoxRemotes.Text = value; } }

        private void RemotesComboboxControl_Load(object sender, EventArgs e)
        {
            if (Site != null && Site.DesignMode)
            {
                return;
            }

            comboBoxRemotes.DataSource = Module.GetRemotes();
        }
    }
}

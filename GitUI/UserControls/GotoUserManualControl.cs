using System;
using System.Windows.Forms;
using ResourceManager;

namespace GitUI.UserControls
{
    public partial class GotoUserManualControl : GitExtensionsControl
    {
        private readonly TranslationString _gotoUserManualControlTooltip =
            new TranslationString("Read more about this feature at {0}");

        private string _manualSectionAnchorName;

        private string _manualSectionSubfolder;

        private bool isLoaded = false;

        public GotoUserManualControl()
        {
            InitializeComponent();
            Translate();
        }

        public string ManualSectionAnchorName
        {
            get { return _manualSectionAnchorName; }
            set { _manualSectionAnchorName = value; if (isLoaded) { UpdateTooltip(); } }
        }

        public string ManualSectionSubfolder
        {
            get { return _manualSectionSubfolder; }
            set { _manualSectionSubfolder = value; if (isLoaded) { UpdateTooltip(); } }
        }

        private string GetUrl()
        {
            return UserManual.UserManual.UrlFor(ManualSectionSubfolder, ManualSectionAnchorName);
        }

        private void GotoUserManualControl_Load(object sender, EventArgs e)
        {
            isLoaded = true;
            UpdateTooltip();
        }

        private void labelHelpIcon_Click(object sender, EventArgs e)
        {
            OpenManual();
        }

        private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenManual();
        }

        private void OpenManual()
        {
            string url = GetUrl();
            OsShellUtil.OpenUrlInDefaultBrowser(url);
        }

        private void UpdateTooltip()
        {
            string caption = string.Format(_gotoUserManualControlTooltip.Text, GetUrl());
            toolTip1.SetToolTip(pictureBoxHelpIcon, caption);
            toolTip1.SetToolTip(linkLabelHelp, caption);
        }
    }
}

namespace GitUI.CommandsDialogs.SettingsDialog.Pages
{
    public partial class HotkeysSettingsPage : SettingsPageWithHeader
    {
        public HotkeysSettingsPage()
        {
            InitializeComponent();
            Text = "Hotkeys";
            Translate();
        }

        protected override void PageToSettings()
        {
            controlHotkeys.SaveSettings();
        }

        protected override void SettingsToPage()
        {
            controlHotkeys.ReloadSettings();
        }
    }
}

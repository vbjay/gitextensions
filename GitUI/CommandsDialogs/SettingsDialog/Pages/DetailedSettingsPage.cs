using GitCommands.Settings;

namespace GitUI.CommandsDialogs.SettingsDialog.Pages
{
    public partial class DetailedSettingsPage : AutoLayoutSettingsPage
    {
        public DetailedSettingsPage()
        {
            InitializeComponent();
            Text = "Detailed";
            Translate();
        }

        private DetailedGroup DetailedSettings
        {
            get
            {
                return RepoDistSettingsSet.RepoDistSettings.Detailed;
            }
        }

        public static SettingsPageReference GetPageReference()
        {
            return new SettingsPageReferenceByType(typeof(DetailedSettingsPage));
        }

        protected override void Init(ISettingsPageHost aPageHost)
        {
            base.Init(aPageHost);
            CreateSettingsControls();
            Translate();
        }

        private void CreateSettingsControls()
        {
            GroupBoxSettingsLayout main = new GroupBoxSettingsLayout(this, "Browse repository window");
            AddSettingsLayout(main);
            main.AddBoolSetting("Show the Console tab", DetailedSettings.ShowConEmuTab);
        }
    }
}

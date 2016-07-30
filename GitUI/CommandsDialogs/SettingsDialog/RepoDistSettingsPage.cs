using GitCommands.Settings;

namespace GitUI.CommandsDialogs.SettingsDialog
{
    public class RepoDistSettingsPage : SettingsPageWithHeader, IRepoDistSettingsPage
    {
        public bool AreEffectiveSettingsSet
        {
            get { return CurrentSettings == RepoDistSettingsSet.EffectiveSettings; }
        }

        protected RepoDistSettings CurrentSettings { get; private set; }
        protected RepoDistSettingsSet RepoDistSettingsSet { get { return CommonLogic.RepoDistSettingsSet; } }

        public void SetEffectiveSettings()
        {
            if (RepoDistSettingsSet != null)
                SetCurrentSettings(RepoDistSettingsSet.EffectiveSettings);
        }

        public override void SetGlobalSettings()
        {
            if (RepoDistSettingsSet != null)
                SetCurrentSettings(RepoDistSettingsSet.GlobalSettings);
        }

        public void SetLocalSettings()
        {
            if (RepoDistSettingsSet != null)
                SetCurrentSettings(RepoDistSettingsSet.LocalSettings);
        }

        public void SetRepoDistSettings()
        {
            if (RepoDistSettingsSet != null)
                SetCurrentSettings(RepoDistSettingsSet.RepoDistSettings);
        }

        protected override void Init(ISettingsPageHost aPageHost)
        {
            base.Init(aPageHost);

            CurrentSettings = RepoDistSettingsSet.EffectiveSettings;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // RepoDistSettingsPage
            //
            this.Name = "RepoDistSettingsPage";
            this.Size = new System.Drawing.Size(951, 518);
            this.ResumeLayout(false);
        }

        private void SetCurrentSettings(RepoDistSettings settings)
        {
            if (CurrentSettings != null)
                SaveSettings();

            CurrentSettings = settings;

            LoadSettings();
        }
    }
}

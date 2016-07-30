using GitCommands.Settings;

namespace GitUI.CommandsDialogs.SettingsDialog
{
    public class ConfigFileSettingsPage : SettingsPageWithHeader, ILocalSettingsPage
    {
        protected ConfigFileSettingsSet ConfigFileSettingsSet { get { return CommonLogic.ConfigFileSettingsSet; } }
        protected ConfigFileSettings CurrentSettings { get; private set; }

        public void SetEffectiveSettings()
        {
            if (ConfigFileSettingsSet != null)
                SetCurrentSettings(ConfigFileSettingsSet.EffectiveSettings);
        }

        public override void SetGlobalSettings()
        {
            if (ConfigFileSettingsSet != null)
                SetCurrentSettings(ConfigFileSettingsSet.GlobalSettings);
        }

        public void SetLocalSettings()
        {
            if (ConfigFileSettingsSet != null)
                SetCurrentSettings(ConfigFileSettingsSet.LocalSettings);
        }

        protected override void Init(ISettingsPageHost aPageHost)
        {
            base.Init(aPageHost);

            CurrentSettings = CommonLogic.ConfigFileSettingsSet.EffectiveSettings;
        }

        private void SetCurrentSettings(ConfigFileSettings settings)
        {
            if (CurrentSettings != null)
                SaveSettings();

            CurrentSettings = settings;

            LoadSettings();
        }
    }
}

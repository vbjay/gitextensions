using GitCommands.Settings;

namespace GitUI.CommandsDialogs.SettingsDialog
{
    public class RepoDistSettingsSet
    {
        public readonly RepoDistSettings EffectiveSettings;
        public readonly RepoDistSettings GlobalSettings;
        public readonly RepoDistSettings LocalSettings;
        public readonly RepoDistSettings RepoDistSettings;

        public RepoDistSettingsSet(
            RepoDistSettings aEffectiveSettings,
            RepoDistSettings aLocalSettings,
            RepoDistSettings aPulledSettings,
            RepoDistSettings aGlobalSettings)
        {
            EffectiveSettings = aEffectiveSettings;
            LocalSettings = aLocalSettings;
            RepoDistSettings = aPulledSettings;
            GlobalSettings = aGlobalSettings;
        }
    }
}

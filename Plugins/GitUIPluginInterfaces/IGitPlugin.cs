using System.Collections.Generic;

namespace GitUIPluginInterfaces
{
    public interface IGitPlugin
    {
        string Description { get; }
        string Name { get; }
        IGitPluginSettingsContainer SettingsContainer { get; set; }

        bool Execute(GitUIBaseEventArgs gitUiCommands);

        IEnumerable<ISetting> GetSettings();

        void Register(IGitUICommands gitUiCommands);

        void Unregister(IGitUICommands gitUiCommands);
    }
}

namespace GitUI.CommandsDialogs.SettingsDialog
{
    public interface ISettingsPageHost
    {
        CheckSettingsLogic CheckSettingsLogic { get; }

        void GotoPage(SettingsPageReference settingsPageReference);

        /// <summary>
        /// needed by ChecklistSettingsPage (TODO: needed here?)
        /// </summary>
        void LoadAll();

        /// <summary>
        /// needed by ChecklistSettingsPage (TODO: needed here?)
        /// </summary>
        void SaveAll();
    }

    public class SettingsPageHostMock : ISettingsPageHost
    {
        private readonly CheckSettingsLogic _CheckSettingsLogic;

        public SettingsPageHostMock(CheckSettingsLogic aCheckSettingsLogic)
        {
            _CheckSettingsLogic = aCheckSettingsLogic;
        }

        public CheckSettingsLogic CheckSettingsLogic { get { return _CheckSettingsLogic; } }

        public void GotoPage(SettingsPageReference settingsPageReference)
        {
        }

        public void LoadAll()
        {
        }

        public void SaveAll()
        {
        }
    }
}

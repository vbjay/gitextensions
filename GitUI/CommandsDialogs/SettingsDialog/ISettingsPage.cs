using System.Collections.Generic;
using System.Windows.Forms;

namespace GitUI.CommandsDialogs.SettingsDialog
{
    public interface ISettingsPage
    {
        Control GuiControl { get; }

        /// <summary>
        /// true if the page cannot properly react to cancel or discard
        /// </summary>
        bool IsInstantSavePage { get; }

        SettingsPageReference PageReference { get; }

        IEnumerable<string> GetSearchKeywords();

        string GetTitle();

        void LoadSettings();

        void OnPageShown();

        void SaveSettings();
    }
}

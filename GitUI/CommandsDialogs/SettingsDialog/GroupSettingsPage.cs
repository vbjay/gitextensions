using System.Collections.Generic;
using System.Windows.Forms;
using ResourceManager;

namespace GitUI.CommandsDialogs.SettingsDialog
{
    /// <summary>
    /// Page to group other pages
    /// </summary>
    public abstract class GroupSettingsPage : Translate, ISettingsPage
    {
        protected GroupSettingsPage(string aTitle)
        {
            Title = aTitle;
            Translator.Translate(this, GitCommands.AppSettings.CurrentTranslation);
        }

        public Control GuiControl { get { return null; } }

        public bool IsInstantSavePage
        {
            get { return false; }
        }

        public SettingsPageReference PageReference
        {
            get { return new SettingsPageReferenceByType(GetType()); }
        }

        public string Title { get; private set; }

        public IEnumerable<string> GetSearchKeywords()
        {
            return new string[] { };
        }

        public string GetTitle()
        {
            return Title;
        }

        public void LoadSettings()
        {
        }

        public void OnPageShown()
        {
        }

        public void SaveSettings()
        {
        }
    }
}

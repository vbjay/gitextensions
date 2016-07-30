using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GitCommands;
using ResourceManager;

namespace GitUI.CommandsDialogs.SettingsDialog
{
    /// <summary>
    /// set Text property in derived classes to set the title
    /// </summary>
    public class SettingsPageBase : GitExtensionsControl, ISettingsPage
    {
        private bool _loadingSettings;
        private ISettingsPageHost _PageHost;

        private IList<string> childrenText;

        public virtual Control GuiControl { get { return this; } }

        public virtual bool IsInstantSavePage
        {
            get { return false; }
        }

        public virtual SettingsPageReference PageReference
        {
            get { return new SettingsPageReferenceByType(GetType()); }
        }

        protected CheckSettingsLogic CheckSettingsLogic { get { return PageHost.CheckSettingsLogic; } }

        protected CommonLogic CommonLogic { get { return CheckSettingsLogic.CommonLogic; } }

        /// <summary>
        /// True during execution of LoadSettings(). Usually derived classes
        /// apply settings to GUI controls. Some of controls trigger events -
        /// IsLoadingSettings can be used for example to not execute the event action.
        /// </summary>
        protected bool IsLoadingSettings
        {
            get { return _loadingSettings; }
        }

        protected GitModule Module { get { return this.CommonLogic.Module; } }

        protected ISettingsPageHost PageHost
        {
            get
            {
                if (_PageHost == null)
                    throw new InvalidOperationException("PageHost instance was not passed to page: " + GetType().FullName);

                return _PageHost;
            }
        }

        public static T Create<T>(ISettingsPageHost aPageHost) where T : SettingsPageBase, new()
        {
            T result = new T();

            result.Init(aPageHost);

            return result;
        }

        /// <summary>
        /// override to provide search keywords
        /// </summary>
        public virtual IEnumerable<string> GetSearchKeywords()
        {
            return childrenText ?? (childrenText = GetChildrenText(this));
        }

        public virtual string GetTitle()
        {
            return Text;
        }

        public void LoadSettings()
        {
            _loadingSettings = true;
            SettingsToPage();
            _loadingSettings = false;
        }

        /// <summary>
        /// Called when SettingsPage is shown (again);
        /// e. g. after user clicked a tree item
        /// </summary>
        public virtual void OnPageShown()
        {
            // to be overridden
        }

        public void SaveSettings()
        {
            PageToSettings();
        }

        protected virtual string GetCommaSeparatedKeywordList()
        {
            return "";
        }

        protected virtual void Init(ISettingsPageHost aPageHost)
        {
            _PageHost = aPageHost;
        }

        protected virtual void PageToSettings()
        {
        }

        protected virtual void SettingsToPage()
        {
        }

        /// <summary>Recursively gets the text from all <see cref="Control"/>s within the specified <paramref name="control"/>.</summary>
        private static IList<string> GetChildrenText(Control control)
        {
            if (control.HasChildren == false) { return new string[0]; }

            List<string> texts = new List<string>();
            foreach (Control child in control.Controls)
            {
                if (!child.Visible || child is NumericUpDown)
                {// skip: invisible; some input controls
                    continue;
                }

                if (child.Enabled && !string.IsNullOrWhiteSpace(child.Text))
                {// enabled AND not whitespace -> add
                    // also searches text boxes and comboboxes
                    // TODO(optional): search through the drop down list of comboboxes
                    // TODO(optional): convert numeric dropdown values to text
                    texts.Add(child.Text.Trim().ToLowerInvariant());
                }
                texts.AddRange(GetChildrenText(child));// recurse
            }
            return texts;
        }
    }
}

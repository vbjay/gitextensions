using System.Collections.Generic;
using GitUI.CommandsDialogs.BrowseDialog;

namespace GitUI.CommandsDialogs
{
    internal class FormBrowseMenuCommands : MenuCommandsBase
    {
        private FormBrowse _formBrowse;

        // must be created only once because of translation
        private IEnumerable<MenuCommand> _navigateMenuCommands;

        public FormBrowseMenuCommands(FormBrowse formBrowse)
        {
            TranslationCategoryName = "FormBrowse";
            Translate();

            _formBrowse = formBrowse;
        }

        private GitUICommands UICommands
        { get { return _formBrowse.UICommands; } }

        public IEnumerable<MenuCommand> GetNavigateMenuCommands()
        {
            if (_navigateMenuCommands == null)
            {
                _navigateMenuCommands = CreateNavigateMenuCommands();
            }

            return _navigateMenuCommands;
        }

        protected override IEnumerable<MenuCommand> GetMenuCommandsForTranslation()
        {
            return GetNavigateMenuCommands();
        }

        private IEnumerable<MenuCommand> CreateNavigateMenuCommands()
        {
            var resultList = new List<MenuCommand>();

            // no additional MenuCommands that are not defined in the RevisionGrid

            return resultList;
        }
    }
}

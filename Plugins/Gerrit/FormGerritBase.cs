using System;
using GitUI;
using GitUIPluginInterfaces;

namespace Gerrit
{
    public class FormGerritBase : GitExtensionsForm
    {
        protected readonly IGitUICommands UICommands;

        protected FormGerritBase(IGitUICommands agitUiCommands)
            : base(true)
        {
            UICommands = agitUiCommands;
        }

        private FormGerritBase()
            : this(null)
        { }

        protected IGitModule Module { get { return UICommands.GitModule; } }
        protected GerritSettings Settings { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
                return;

            Settings = GerritSettings.Load(Module);

            if (Settings == null)
            {
                Dispose();
                return;
            }

            base.OnLoad(e);
        }
    }
}

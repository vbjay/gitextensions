// Copyright (C) 2006-2008 Jim Tilander. See COPYING for and README for more details.
using EnvDTE;
using EnvDTE80;

namespace GitPlugin.Commands
{
    internal class ToolbarCommand<ItemCommandT> : CommandBase
        where ItemCommandT : ItemCommandBase, new()
    {
        public ToolbarCommand(bool runForSelection = false)
        {
            RunForSelection = runForSelection;
        }

        public override bool IsEnabled(DTE2 application)
        {
            return new ItemCommandT().IsEnabled(application);
        }

        public override void OnCommand(DTE2 application, OutputWindowPane pane)
        {
            var command = new ItemCommandT { RunForSelection = RunForSelection };
            command.OnCommand(application, pane);
        }
    }
}

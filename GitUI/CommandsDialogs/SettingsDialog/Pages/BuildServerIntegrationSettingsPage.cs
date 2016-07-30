using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitUIPluginInterfaces;
using GitUIPluginInterfaces.BuildServerIntegration;
using ResourceManager;

namespace GitUI.CommandsDialogs.SettingsDialog.Pages
{
    public partial class BuildServerIntegrationSettingsPage : RepoDistSettingsPage
    {
        private readonly TranslationString _noneItem =
            new TranslationString("None");

        private Task<object> _populateBuildServerTypeTask;

        public BuildServerIntegrationSettingsPage()
        {
            InitializeComponent();
            Text = "Build server integration";
            Translate();
        }

        public override bool IsInstantSavePage
        {
            get { return false; }
        }

        protected override void Init(ISettingsPageHost aPageHost)
        {
            base.Init(aPageHost);

            _populateBuildServerTypeTask =
                Task.Factory.StartNew(() =>
                        {
                            var exports = ManagedExtensibility.GetExports<IBuildServerAdapter, IBuildServerTypeMetadata>();
                            var buildServerTypes = exports.Select(export =>
                                {
                                    var canBeLoaded = export.Metadata.CanBeLoaded;
                                    return export.Metadata.BuildServerType.Combine(" - ", canBeLoaded);
                                }).ToArray();

                            return buildServerTypes;
                        })
                    .ContinueWith(
                        task =>
                            {
                                checkBoxEnableBuildServerIntegration.Enabled = true;
                                checkBoxShowBuildSummary.Enabled = true;
                                BuildServerType.Enabled = true;

                                BuildServerType.DataSource = new[] { _noneItem.Text }.Concat(task.Result).ToArray();
                                return BuildServerType.DataSource;
                            },
                        TaskScheduler.FromCurrentSynchronizationContext());
        }

        protected override void PageToSettings()
        {
            CurrentSettings.BuildServer.EnableIntegration.Value = checkBoxEnableBuildServerIntegration.GetNullableChecked();
            CurrentSettings.BuildServer.ShowBuildSummaryInGrid.Value = checkBoxShowBuildSummary.GetNullableChecked();

            var selectedBuildServerType = GetSelectedBuildServerType();

            CurrentSettings.BuildServer.Type.Value = selectedBuildServerType;

            var control =
                buildServerSettingsPanel.Controls.OfType<IBuildServerSettingsUserControl>()
                                        .SingleOrDefault();
            if (control != null)
                control.SaveSettings(CurrentSettings.BuildServer.TypeSettings);
        }

        protected override void SettingsToPage()
        {
            _populateBuildServerTypeTask.ContinueWith(
                task =>
                {
                    checkBoxEnableBuildServerIntegration.SetNullableChecked(CurrentSettings.BuildServer.EnableIntegration.Value);
                    checkBoxShowBuildSummary.SetNullableChecked(CurrentSettings.BuildServer.ShowBuildSummaryInGrid.Value);

                    BuildServerType.SelectedItem = CurrentSettings.BuildServer.Type.Value ?? _noneItem.Text;
                },
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ActivateBuildServerSettingsControl()
        {
            var controls = buildServerSettingsPanel.Controls.OfType<IBuildServerSettingsUserControl>().Cast<Control>();
            var previousControl = controls.SingleOrDefault();
            if (previousControl != null) previousControl.Dispose();

            var control = CreateBuildServerSettingsUserControl();

            buildServerSettingsPanel.Controls.Clear();

            if (control != null)
            {
                control.LoadSettings(CurrentSettings.BuildServer.TypeSettings);

                buildServerSettingsPanel.Controls.Add((Control)control);
            }
        }

        private void BuildServerType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ActivateBuildServerSettingsControl();
        }

        private IBuildServerSettingsUserControl CreateBuildServerSettingsUserControl()
        {
            if (BuildServerType.SelectedIndex == 0 || string.IsNullOrEmpty(Module.WorkingDir))
                return null;
            var defaultProjectName = Module.WorkingDir.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).Last();

            var exports = ManagedExtensibility.GetExports<IBuildServerSettingsUserControl, IBuildServerTypeMetadata>();
            var selectedExport = exports.SingleOrDefault(export => export.Metadata.BuildServerType == GetSelectedBuildServerType());
            if (selectedExport != null)
            {
                var buildServerSettingsUserControl = selectedExport.Value;
                buildServerSettingsUserControl.Initialize(defaultProjectName);
                return buildServerSettingsUserControl;
            }

            return null;
        }

        private string GetSelectedBuildServerType()
        {
            if (BuildServerType.SelectedIndex == 0)
                return null;
            return (string)BuildServerType.SelectedItem;
        }
    }
}

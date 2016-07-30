using System.Collections.Generic;
using GitUIPluginInterfaces;
using ResourceManager;

namespace FindLargeFiles
{
    public class FindLargeFilesPlugin : GitPluginBase, IGitPluginForRepository
    {
        private NumberSetting<float> SizeLargeFile = new NumberSetting<float>("Find large files bigger than (Mb)", 1);

        public FindLargeFilesPlugin()
        {
            SetNameAndDescription("Find large files");
            Translate();
        }

        public override bool Execute(GitUIBaseEventArgs gitUiCommands)
        {
            using (var frm = new FindLargeFilesForm(SizeLargeFile.ValueOrDefault(Settings), gitUiCommands))
                frm.ShowDialog(gitUiCommands.OwnerForm);
            return true;
        }

        public override IEnumerable<ISetting> GetSettings()
        {
            //return all settings or introduce implementation based on reflection on GitPluginBase level
            yield return SizeLargeFile;
        }
    }
}

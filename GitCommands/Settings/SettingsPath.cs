using System;
using GitUIPluginInterfaces;

namespace GitCommands.Settings
{
    public class SettingsPath : ISettingsSource
    {
        public readonly ISettingsSource Parent;
        public readonly string PathName;
        private const string PathSep = ".";

        public SettingsPath(ISettingsSource aParent, string aPathName)
        {
            Parent = aParent;
            PathName = aPathName;
        }

        public override T GetValue<T>(string name, T defaultValue, Func<string, T> decode)
        {
            return Parent.GetValue(PathFor(name), defaultValue, decode);
        }

        public virtual string PathFor(string subPath)
        {
            return PathName + PathSep + subPath;
        }

        public override void SetValue<T>(string name, T value, Func<T, string> encode)
        {
            Parent.SetValue(PathFor(name), value, encode);
        }
    }
}

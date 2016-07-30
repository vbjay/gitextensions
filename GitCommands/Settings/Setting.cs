namespace GitCommands.Settings
{
    public abstract class Setting<T>
    {
        public readonly T DefaultValue;
        public readonly string Name;
        public readonly SettingsPath SettingsSource;

        public Setting(string aName, SettingsPath aSettingsSource, T aDefaultValue)
        {
            Name = aName;
            SettingsSource = aSettingsSource;
            DefaultValue = aDefaultValue;
        }

        public string FullPath
        {
            get
            {
                return SettingsSource.PathFor(Name);
            }
        }

        public abstract T Value { get; set; }
    }
}

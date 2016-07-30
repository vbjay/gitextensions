using System.Windows.Forms;

namespace GitUIPluginInterfaces
{
    public interface ISetting
    {
        /// <summary>
        /// Caption of the setting
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Name of the setting
        /// </summary>
        string Name { get; }

        ISettingControlBinding CreateControlBinding();
    }

    public interface ISettingControlBinding
    {
        /// <summary>
        /// returns caption assotiated with this control or null if the control layouts
        /// the caption by itself
        /// </summary>
        string Caption();

        /// <summary>
        /// Creates a control to be placed on FormSettings to edit this setting value
        /// Control should take care of scalability and resizability of its subcontrols
        /// </summary>
        /// <returns></returns>
        Control GetControl();

        ISetting GetSetting();

        /// <summary>
        /// Loads setting value from settings to Control
        /// </summary>
        /// <param name="settings"></param>
        void LoadSetting(ISettingsSource settings, bool areSettingsEffective);

        /// <summary>
        /// Saves value from Control to settings
        /// </summary>
        /// <param name="settings"></param>
        void SaveSetting(ISettingsSource settings);
    }

    public abstract class SettingControlBinding<S, T> : ISettingControlBinding where T : Control where S : ISetting
    {
        protected readonly S Setting;
        private T _control;

        protected SettingControlBinding(S aSetting)
        {
            Setting = aSetting;
        }

        private T Control
        {
            get
            {
                if (_control == null)
                    _control = CreateControl();

                return _control;
            }
        }

        public virtual string Caption()
        {
            return Setting.Caption;
        }

        /// <summary>
        /// Creates a control to be placed on FormSettings to edit this setting value
        /// Control should take care of scalability and resizability of its subcontrols
        /// </summary>
        /// <returns></returns>
        public abstract T CreateControl();

        public Control GetControl()
        {
            return Control;
        }

        public ISetting GetSetting()
        {
            return Setting;
        }

        public void LoadSetting(ISettingsSource settings, bool areSettingsEffective)
        {
            LoadSetting(settings, areSettingsEffective, Control);
        }

        /// <summary>
        /// Loads setting value from settings to Control
        /// </summary>
        public abstract void LoadSetting(ISettingsSource settings, bool areSettingsEffective, T control);

        /// <summary>
        /// Saves value from Control to settings
        /// </summary>
        /// <param name="settings"></param>
        public void SaveSetting(ISettingsSource settings)
        {
            SaveSetting(settings, Control);
        }

        /// <summary>
        /// Saves value from Control to settings
        /// </summary>
        public abstract void SaveSetting(ISettingsSource settings, T control);
    }
}

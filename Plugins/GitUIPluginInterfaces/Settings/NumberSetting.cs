using System.Windows.Forms;

namespace GitUIPluginInterfaces
{
    public class NumberSetting<T> : ISetting
    {
        public NumberSetting(string aName, T aDefaultValue)
            : this(aName, aName, aDefaultValue)
        {
        }

        public NumberSetting(string aName, string aCaption, T aDefaultValue)
        {
            Name = aName;
            Caption = aCaption;
            DefaultValue = aDefaultValue;
        }

        public string Caption { get; private set; }
        public T DefaultValue { get; set; }
        public string Name { get; private set; }

        public object this[ISettingsSource settings]
        {
            get
            {
                return settings.GetValue(Name, null, s =>
                    {
                        return ConvertFromString(s);
                    });
            }

            set
            {
                settings.SetValue(Name, value, i => { return ConvertToString(i); });
            }
        }

        public ISettingControlBinding CreateControlBinding()
        {
            return new TextBoxBinding(this);
        }

        public T ValueOrDefault(ISettingsSource settings)
        {
            object settingVal = this[settings];
            if (settingVal == null)
            {
                return DefaultValue;
            }
            else
            {
                return (T)settingVal;
            }
        }

        private static object ConvertFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            var type = typeof(T);
            if (type == typeof(int))
                return int.Parse(value);
            if (type == typeof(float))
                return float.Parse(value);
            if (type == typeof(double))
                return double.Parse(value);
            if (type == typeof(long))
                return long.Parse(value);
            return null;
        }

        private static string ConvertToString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString();
        }

        private class TextBoxBinding : SettingControlBinding<NumberSetting<T>, TextBox>
        {
            public TextBoxBinding(NumberSetting<T> aSetting)
                : base(aSetting)
            { }

            public override TextBox CreateControl()
            {
                return new TextBox();
            }

            public override void LoadSetting(ISettingsSource settings, bool areSettingsEffective, TextBox control)
            {
                object settingVal;
                if (areSettingsEffective)
                {
                    settingVal = Setting.ValueOrDefault(settings);
                }
                else
                {
                    settingVal = Setting[settings];
                }

                control.Text = ConvertToString(settingVal);
            }

            public override void SaveSetting(ISettingsSource settings, TextBox control)
            {
                Setting[settings] = ConvertFromString(control.Text);
            }
        }
    }
}

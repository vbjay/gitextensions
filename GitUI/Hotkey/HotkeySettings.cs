using System;
using System.Xml.Serialization;
using ResourceManager;

namespace GitUI.Hotkey
{
    /// <summary>
    /// Stores all hotkey mappings of one target
    /// </summary>
    [Serializable]
    public class HotkeySettings
    {
        public HotkeySettings()
        {
        }

        public HotkeySettings(string name, params HotkeyCommand[] commands)
        {
            this.Name = name;
            this.Commands = commands;
        }

        [XmlArray]
        public HotkeyCommand[] Commands { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
    }
}

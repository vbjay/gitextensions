﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GitUI
{
    /// <summary>
    ///   Stores the state and position of a single window
    /// </summary>
    [DebuggerDisplay("Name={Name} Rect={Rect} State={State}")]
    [Serializable]
    public class WindowPosition
    {
        public WindowPosition(Rectangle rect, FormWindowState state, string name)
        {
            Rect = rect;
            State = state;
            Name = name;
        }

        protected WindowPosition()
        {
        }

        public string Name { get; set; }
        public Rectangle Rect { get; set; }
        public FormWindowState State { get; set; }
    }

    [Serializable]
    public class WindowPositionList
    {
        private static readonly string AppDataDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GitExtensions");

        private static readonly string ConfigFilePath = Path.Combine(AppDataDir, "WindowPositions.xml");

        static WindowPositionList()
        {
            if (!Directory.Exists(AppDataDir))
            {
                Directory.CreateDirectory(AppDataDir);
            }
        }

        protected WindowPositionList()
        {
            WindowPositions = new List<WindowPosition>();
        }

        public List<WindowPosition> WindowPositions { get; set; }

        public static WindowPositionList Load()
        {
            if (!File.Exists(ConfigFilePath))
            {
                return new WindowPositionList();
            }
            try
            {
                using (
                    var stream = File.Open(ConfigFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {
                    return new XmlSerializer(typeof(WindowPositionList)).Deserialize(stream) as WindowPositionList;
                }
            }
            catch
            {
                return new WindowPositionList();
            }
        }

        public void AddOrUpdate(WindowPosition pos)
        {
            WindowPositions.RemoveAll(r => r.Name == pos.Name);
            WindowPositions.Add(pos);
        }

        public WindowPosition Get(string name)
        {
            return WindowPositions.FirstOrDefault(r => r.Name == name);
        }

        public void Save()
        {
            using (var stream = File.Open(ConfigFilePath, FileMode.Create, FileAccess.Write))
            {
                new XmlSerializer(typeof(WindowPositionList)).Serialize(stream, this);
            }
        }
    }
}

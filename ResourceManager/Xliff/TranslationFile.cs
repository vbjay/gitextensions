﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ResourceManager.Xliff
{
    /// <summary>Provides a translation for a specific language.</summary>
    [XmlRoot("xliff")]
    public class TranslationFile : ITranslation
    {
        private string _languageCode;

        public TranslationFile()
        {
            Version = "1.0";
            TranslationCategories = new List<TranslationCategory>();
        }

        public TranslationFile(string gitExVersion, string languageCode)
            : this()
        {
            GitExVersion = gitExVersion;
            _languageCode = languageCode;
        }

        [XmlAttribute("GitExVersion")]
        public string GitExVersion { get; set; }

        [XmlAttribute("LanguageCode")]
        public string LanguageCode { get { return _languageCode; } }

        [XmlElement(ElementName = "file")]
        public List<TranslationCategory> TranslationCategories { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        public void AddTranslationCategory(TranslationCategory translationCategory)
        {
            if (string.IsNullOrEmpty(translationCategory.Name))
                throw new InvalidOperationException("Cannot add translationCategory without name");

            TranslationCategories.Add(translationCategory);
        }

        public void AddTranslationItem(string category, string item, string property, string neutralValue)
        {
            FindOrAddTranslationCategory(category).Body.AddTranslationItemIfNotExist(new TranslationItem(item, property, neutralValue));
        }

        public TranslationCategory FindOrAddTranslationCategory(string translationCategory)
        {
            TranslationCategory tc = GetTranslationCategory(translationCategory);
            if (tc == null)
            {
                tc = new TranslationCategory(translationCategory, "en");
                AddTranslationCategory(tc);
            }
            return tc;
        }

        public TranslationCategory GetTranslationCategory(string name)
        {
            return TranslationCategories.Find(t => t.Name.TrimStart('_') == name.TrimStart('_'));
        }

        public void Sort()
        {
            TranslationCategories.Sort();
            foreach (TranslationCategory tc in TranslationCategories)
                tc.Body.TranslationItems.Sort();
        }

        public string TranslateItem(string category, string item, string property, Func<string> provideDefaultValue)
        {
            TranslationCategory tc = FindOrAddTranslationCategory(category);

            TranslationItem ti = tc.Body.GetTranslationItem(item, property);

            if (ti == null)
            {
                //if an item is not translated, then store its default value
                //to be able to retrieve it later (eg. when to a caption
                //is added an additional information like 'Commit (<number of changes>)',
                //and then the caption needs to be refreshed)
                string defaultValue = provideDefaultValue();
                tc.Body.AddTranslationItemIfNotExist(new TranslationItem(item, property, defaultValue));
                return defaultValue;
            }

            if (string.IsNullOrEmpty(ti.Value))
                return ti.Source;

            return ti.Value;
        }
    }
}

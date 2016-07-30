using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ResourceManager.Xliff
{
    [DebuggerDisplay("{name}.{property}={value}")]
    public class TranslationItem : IComparable<TranslationItem>, ICloneable
    {
        private string _name;

        private string _property;

        private string _source;

        private string _value;

        public TranslationItem()
        {
        }

        public TranslationItem(string name, string property, string source)
        {
            _name = name;
            _property = property;
            _source = source;
        }

        public TranslationItem(string name, string property, string source, string value)
        {
            _name = name;
            _property = property;
            _source = source;
            _value = value;
        }

        [XmlAttribute("id")]
        public string Id
        {
            get
            {
                return _name + "." + _property;
            }
            set
            {
                var vals = value.Split(new[] { '.' }, 2);
                _name = vals[0];
                _property = vals.Length > 1 ? vals[1] : "";
            }
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [XmlIgnore]
        public string Property
        {
            get
            {
                return _property;
            }
            set
            {
                _property = value;
            }
        }

        [XmlElement("source")]
        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        [XmlElement("target")]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public TranslationItem Clone()
        {
            return new TranslationItem(_name, _property, _source, _value);
        }

        public int CompareTo(TranslationItem other)
        {
            int val = String.Compare(Name, other.Name, StringComparison.Ordinal);
            if (val == 0) val = String.Compare(Property, other.Property, StringComparison.Ordinal);
            return val;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}

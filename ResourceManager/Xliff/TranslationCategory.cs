using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ResourceManager.Xliff
{
    [DebuggerDisplay("{_name}")]
    public class TranslationCategory : IComparable<TranslationCategory>
    {
        private TranslationBody _body = new TranslationBody();

        private string _datatype = "plaintext";

        private string _name;

        private string _source;

        public TranslationCategory()
        {
        }

        public TranslationCategory(string name, string source)
        {
            this._name = name;
            this._source = source;
        }

        [XmlElement(ElementName = "body")]
        public TranslationBody Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        [XmlAttribute("datatype")]
        public string Datatype
        {
            get
            {
                return _datatype;
            }
            set
            {
                _datatype = value;
            }
        }

        [XmlAttribute("original")]
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

        [XmlAttribute("source-language")]
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

        public int CompareTo(TranslationCategory other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}

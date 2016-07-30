using System.Collections.Generic;

namespace NetSpell.SpellChecker.Dictionary.Affix
{
    using AffixEntryCollection = List<AffixEntry>;

    /// <summary>
    ///		Rule for expanding base words
    /// </summary>
    public class AffixRule
    {
        private AffixEntryCollection _AffixEntries = new AffixEntryCollection();
        private bool _AllowCombine;
        private string _Name = "";

        /// <summary>
        ///     Collection of text entries that make up this rule
        /// </summary>
        public AffixEntryCollection AffixEntries
        {
            get { return _AffixEntries; }
            set { _AffixEntries = value; }
        }

        /// <summary>
        ///     Allow combining prefix and suffix
        /// </summary>
        public bool AllowCombine
        {
            get { return _AllowCombine; }
            set { _AllowCombine = value; }
        }

        /// <summary>
        ///     Name of the Affix rule
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}

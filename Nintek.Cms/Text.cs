using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms
{
    public class Text
    {
        readonly Dictionary<string, string> _translations = new Dictionary<string, string>();

        public Text(string value)
            : this(Language.Default, value)
        {
        }

        public Text(string language, string value)
        {
            _translations.Add(language, value);
        }

        public Text AddTranslation(string language, string value)
        {
            _translations.Add(language, value);
            return this;
        }

        public string Get()
        {
            return _translations[Language.Default];
        }

        public string Get(string language)
        {
            _translations.TryGetValue(language, out var value);
            return value;
        }
    }
}

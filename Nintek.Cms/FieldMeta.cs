using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms
{
    public class FieldMeta
    {
        public string Name { get; }
        public string Type { get; }

        public FieldMeta(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}

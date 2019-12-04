using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms
{
    public class Link
    {
        public string Name { get; }
        public string Url { get; }

        public Link(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}

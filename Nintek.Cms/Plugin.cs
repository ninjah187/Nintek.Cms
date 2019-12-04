using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms
{
    public class Plugin : Model
    {
        public Plugin(string name, string version)
            : this(0, name, version, false)
        {
        }

        [JsonConstructor]
        public Plugin(int id, string name, string version, bool initialized)
            : base(id)
        {
            Name = name;
            Version = version;
            Initialized = initialized;
        }

        public string Name { get; }
        public string Version { get; }
        public bool Initialized { get; }
    }
}

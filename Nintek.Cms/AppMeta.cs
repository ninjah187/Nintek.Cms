using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms
{
    public class AppMeta : Model
    {
        public DateTime CreatedAt { get; }
        public bool Initialized { get; }

        public AppMeta(DateTime createdAt, bool initialized)
        {
            CreatedAt = createdAt;
            Initialized = initialized;
        }
    }
}

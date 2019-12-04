using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintek.Cms.AdminPanel.Models
{
    public class ModelVm
    {
        public ModelVm(Model model, (string key, object value)[] entries)
        {
            Model = model;
            Entries = entries;
        }

        public Model Model { get; }
        public (string key, object value)[] Entries { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms.AdminPanel.Models
{
    public class ModelEditVm
    {
        public ModelEditVm(ModelMeta meta, Model model)
        {
            Meta = meta;
            Model = model;
        }

        public ModelMeta Meta { get; }
        public Model Model { get; }
    }
}

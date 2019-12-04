using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintek.Cms.AdminPanel.Models
{
    public class ModelListVm
    {
        public ModelListVm(ModelMeta meta, ModelVm[] models)
        {
            Meta = meta;
            Models = models;
            Keys = !Models.Any()
                ? Array.Empty<string>()
                : Models.FirstOrDefault().Entries.Select(x => x.key).ToArray();
        }

        public ModelMeta Meta { get; }
        public ModelVm[] Models { get; }
        public string[] Keys { get; }
    }
}

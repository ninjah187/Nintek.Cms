using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms.AdminPanel.Models
{
    public class ModelMetaViewModel
    {
        public ModelMetaViewModel(string name, string slug, int fieldsCount)
        {
            Name = name;
            Slug = slug;
            FieldsCount = fieldsCount;
        }

        public string Name { get; }
        public string Slug { get; }
        public int FieldsCount { get; }
    }
}

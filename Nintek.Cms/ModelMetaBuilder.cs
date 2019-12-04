using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nintek.Cms
{
    public class ModelMetaBuilder
    {
        public ModelMeta[] Build(Type[] types = null)
        {
            types = types ?? Assembly.GetEntryAssembly().GetTypes();
            return types
                .Where(type => type.IsSubclassOf(typeof(Model)))
                .Except(CoreModels())
                .Select(type => new ModelMeta(type.FullName, BuildFields(type)))
                .ToArray();
        }

        static FieldMeta[] BuildFields(Type type)
        {
            return type
                .GetProperties()
                .Select(property => new FieldMeta(property.Name, property.PropertyType.FullName))
                .ToArray();
        }

        static IEnumerable<Type> CoreModels()
        {
            yield return typeof(AppMeta);
            yield return typeof(ModelMeta);
            yield return typeof(Model);
        }
    }
}

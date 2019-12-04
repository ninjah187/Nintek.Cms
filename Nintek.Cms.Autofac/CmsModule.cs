using Autofac;
using System;
using System.Linq;
using Assembly = System.Reflection.Assembly;

namespace Nintek.Cms.Autofac
{
    public class CmsModule : Module
    {
        readonly string _connectionString;

        public CmsModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var modelTypes = Assembly
                .GetEntryAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Model)))
                .ToArray();

            builder
                .Register(context => new Bank(_connectionString, modelTypes))
                .InstancePerLifetimeScope();
        }
    }
}

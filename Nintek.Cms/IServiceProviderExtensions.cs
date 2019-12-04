using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms
{
    public static class IServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T) serviceProvider.GetService(typeof(T));
        }
    }
}

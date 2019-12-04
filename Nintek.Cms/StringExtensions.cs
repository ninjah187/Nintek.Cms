using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string value, string other)
        {
            return value.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms
{
    public static class FieldType
    {
        public const string Id = "id";
        public const string Text = "text";
        public const string Number = "number";

        public static IEnumerable<string> All()
        {
            yield return Id;
            yield return Text;
            yield return Number;
        }
    }
}

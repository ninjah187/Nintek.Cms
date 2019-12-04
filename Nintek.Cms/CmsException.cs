using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms
{
    public class CmsException : Exception
    {
        public CmsException(string message) : base(message)
        {
        }
    }
}

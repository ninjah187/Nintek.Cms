using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nintek.Cms.Web.Models
{
    public class Product : Model
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}

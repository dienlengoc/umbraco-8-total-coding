using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated_Umbraco.Models
{
    public class ContentTypeData
    {
        public string[] Composites { get; set; }
        public PropertyTypeData[] Properties { get; set; }
        public string Template { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated_Umbraco.Models
{
    public class ContentNodePropertyData
    {
        private string _name { get; set; }
        private string _value { get; set; }

        public ContentNodePropertyData(string name, string value)
        {
            _name = name;
            _value = value;
        }
        public string Name { get { return _name; } }
        public string Value { get { return _value; } }
    }
}

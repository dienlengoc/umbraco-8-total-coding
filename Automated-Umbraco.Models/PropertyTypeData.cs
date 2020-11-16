using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated_Umbraco.Models
{
    public class PropertyTypeData
    {
        private string _name { get; set; }
        private string _alias { get; set; }
        private string _dataTypeName { get; set; }

        private string _group { get; set; }

        public PropertyTypeData(string name, string alias, string dataTypeName, string group)
        {
            _name = name;
            _alias = alias;
            _dataTypeName = dataTypeName;
            _group = group;
        }

        public string Name { get { return _name; } }
        public string Alias { get { return _alias; } }

        public string DataTypeName { get { return _dataTypeName; } }
        public string Group { get { return _group; } }
    }
}

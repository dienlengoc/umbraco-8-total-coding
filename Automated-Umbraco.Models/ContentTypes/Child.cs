using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Automated_Umbraco.Models.ContentTypes
{
    public class Child : Component
    {
        private string _name;
        private string _alias;
        private string _icon;

        public Child(string name, string alias, string icon)
        {
            _name = name;
            _alias = alias;
            _icon = icon;
        }
        public override void AddChild(Component component)
        {
            //
        }

        public override ContentType Create(int parentId = -1)
        {
            return new ContentType(parentId) { Alias = _alias, Name = _name, Icon = _icon };
        }
    }
}

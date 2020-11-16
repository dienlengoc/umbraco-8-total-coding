using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Automated_Umbraco.Models.ContentNodes
{
    public class Child : Component
    {
        private string _name { get; set; }
        private IContentType _contentType { get; set; }

        public Child(IContentType contentType, string name)
        {
            _name = name;
            _contentType = contentType;
        }

        public override void AddChild(Component component)
        {
            //
        }

        public override IContent Create(int parentId = -1)
        {
            return new Content(_name, parentId, _contentType);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Automated_Umbraco.Models.ContentTypes
{
    public class Default : Component
    {
        private List<Component> _components;
        private IContentTypeService _contentTypeService;

        public Default(IContentTypeService contentTypeService)
        {
            _components = new List<Component>();
            _contentTypeService = contentTypeService;
        }

        public override void AddChild(Component component)
        {
            _components.Add(component);
        }

        public override ContentType Create(int parentId = -1)
        {
            foreach (var component in _components)
            {
                _contentTypeService.Save(component.Create(parentId));
            }

            return null;
        }
    }
}

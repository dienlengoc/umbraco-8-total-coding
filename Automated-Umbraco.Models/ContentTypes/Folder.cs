using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Automated_Umbraco.Models.ContentTypes
{
    public class Folder : Component
    {
        private List<Component> _components;
        private IContentTypeService _contentTypeService;
        private string _name;

        public Folder(IContentTypeService contentTypeService, string name)
        {
            _components = new List<Component>();
            _contentTypeService = contentTypeService;

            _name = name;
        }

        public override void AddChild(Component component)
        {
            _components.Add(component);
        }

        public override ContentType Create(int parentId = -1)
        {
            var parentContainer = _contentTypeService.CreateContainer(parentId, _name);

            if(parentContainer.Success)
            {
                var parentNode = _contentTypeService.GetContainers(_name, 1).FirstOrDefault();

                foreach (var component in _components)
                {
                    _contentTypeService.Save(component.Create(parentNode.Id));
                }
            }

            return null;
        }
    }
}

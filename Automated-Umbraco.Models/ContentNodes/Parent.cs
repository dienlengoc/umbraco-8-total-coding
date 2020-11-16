using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Automated_Umbraco.Models.ContentNodes
{
    public class Parent : Component
    {
        private List<Component> _components { get; set; }
        private IContentService _contentService { get; set; }
        private IContentType _contentType { get; set; }
        private string _name { get; set; }


        public Parent(IContentService contentService, IContentType contentType, string name)
        {
            _contentService = contentService;
            _contentType = contentType;
            _name = name;

            _components = new List<Component>();
        }

        public override void AddChild(Component component)
        {
            _components.Add(component);
        }

        public override IContent Create(int parentId = -1)
        {
            var parentContentNode = _contentService.Create(_name, parentId, _contentType.Alias);
            _contentService.SaveAndPublish(parentContentNode);

            foreach (var component in _components)
            {
                var newContent = component.Create(parentContentNode.Id);
                _contentService.SaveAndPublish(newContent);
            }

            return parentContentNode;
        }
    }
}

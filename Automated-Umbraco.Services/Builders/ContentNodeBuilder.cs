using Automated_Umbraco.Models.ContentNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Automated_Umbraco.Services.Builders
{
    public class ContentNodeBuilder
    {
        private readonly IContentService _contentService;
        private readonly IContentTypeService _contentTypeService;
        private readonly List<IContent> _newContentNodes;

        private Component _initialComponent;

        private List<Component> _components;
        private Component _component
        {
            get
            {
                if (_components != null && _components.Any())
                    return _components.Last();
                return _initialComponent;
            }
            set {; }
        }


        public ContentNodeBuilder(IContentService contentService, IContentTypeService contentTypeService)
        {
            _contentService = contentService;
            _contentTypeService = contentTypeService;

            _newContentNodes = new List<IContent>();
        }

        public ContentNodeBuilder CreateRoot()
        {
            var homeContentType = _contentTypeService.Get("home");

            _initialComponent = new Parent(_contentService, homeContentType, "Home");
            _components = new List<Component>();

            _components.Add(_component);

            return this;
        }

        public ContentNodeBuilder AddNode(string name, string contentTypeAlias)
        {
            var contentType = _contentTypeService.Get(contentTypeAlias);
            _component.AddChild(new Child(contentType, name));

            return this;
        }

        public ContentNodeBuilder AddParent(string name, string contentTypeAlias)
        {
            var contentType = _contentTypeService.Get(contentTypeAlias);
            var parent = new Parent(_contentService, contentType, name);
            _components.Add(parent);

            return this;
        }

        public void Complete()
        {
            foreach (var component in _components)
            {
                _newContentNodes.Add(component.Create(-1));
            }

            _contentService.EmptyRecycleBin(-1);
        }
    }
}

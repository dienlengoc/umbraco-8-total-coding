using Automated_Umbraco.Models.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Services;

namespace Automated_Umbraco.Services.Builders
{
    public class ContentTypeBuilder
    {
        private readonly IContentTypeService _contentTypeService;

        private List<Component> _components;

        private Component _initialComponent;

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

        public ContentTypeBuilder(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }

        public ContentTypeBuilder Start()
        {
            _initialComponent = new Default(_contentTypeService);
            _components = new List<Component>();

            _components.Add(_component);

            return this;
        }

        public ContentTypeBuilder AddNode(string name, string alias, string icon)
        {
            _component.AddChild(new Child(name, alias, icon));

            return this;
        }

        public ContentTypeBuilder AddFolder(string name)
        {
            var parent = new Folder(_contentTypeService, name);
            _components.Add(parent);

            return this;
        }

        public void Complete()
        {
            foreach (var component in _components)
            {
                component.Create(-1);
            }
        }
    }
}
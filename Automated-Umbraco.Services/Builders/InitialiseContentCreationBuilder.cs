using Automated_Umbraco.Models;
using Automated_Umbraco.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Automated_Umbraco.Services.Builders
{
    public class InitialiseContentCreationBuilder
    {
        private readonly IContentTypeService _contentTypeService;
        private readonly IFileService _fileService;
        private readonly IDataTypeService _dataTypeService;
        private readonly IContentService _contentService;

        public InitialiseContentCreationBuilder(IContentService contentService, 
            IContentTypeService contentTypeService, 
            IFileService fileService, 
            IDataTypeService dataTypeService)
        {
            _contentTypeService = contentTypeService;
            _fileService = fileService;
            _dataTypeService = dataTypeService;
            _contentService = contentService;
        }

        public InitialiseContentCreationBuilder CreateContentTypes()
        {
            var contentTypeBuilder = new ContentTypeBuilder(_contentTypeService);


            contentTypeBuilder.Start()
                              .AddNode("Home", "home", "icon-home")
                              .AddNode("Content Page", "contentPage", "icon-document")
                              .AddFolder("Composites")
                              .AddNode("SEO", "seo", "icon-document")
                              .Complete();

            return this;
        }

        public InitialiseContentCreationBuilder UpdateContentTypes()
        {
            //Usually I wouldnt explcitly instantiate a class here, I would use DI with an IOC container but due to time, I stuck with this
            IContentTypeUpdateService contentTypeUpdateService = new ContentTypeUpdateService(_contentTypeService, _fileService, _dataTypeService);

            var seoContentType = _contentTypeService.Get("seo");
            var homeContentType = _contentTypeService.Get("home");
            var pageContentType = _contentTypeService.Get("contentPage");

            if (seoContentType != null)
            {
                var contentTypeData = new ContentTypeData()
                {
                    Properties = new PropertyTypeData[] { new PropertyTypeData("Page Title", "pageTitle", "Textstring", "SEO") }
                };

                contentTypeUpdateService.Update(seoContentType, contentTypeData);
            }

            if (homeContentType != null)
            {
                var contentTypeData = new ContentTypeData()
                {
                    Template = "Home",
                    Composites = new string[] { "seo" },
                    Properties = new PropertyTypeData[] { new PropertyTypeData("Body", "body", "Richtext editor", "Details") }
                };

                contentTypeUpdateService.Update(homeContentType, contentTypeData);
            }

            if (pageContentType != null)
            {
                var contentTypeData = new ContentTypeData()
                {
                    Template = "Content Page",
                    Composites = new string[] { "seo" },
                    Properties = new PropertyTypeData[] { new PropertyTypeData("Body", "body", "Richtext editor", "Details") }
                };

                contentTypeUpdateService.Update(pageContentType, contentTypeData);
            }

            contentTypeUpdateService.SetAllowedContentTypes(homeContentType, new IContentType[] { pageContentType });

            return this;
        }

        public InitialiseContentCreationBuilder CreateContentNodes()
        {
            var contentNodeBuilder = new ContentNodeBuilder(_contentService, _contentTypeService);

           contentNodeBuilder
                                .CreateRoot()
                                .AddNode("About Us", "contentPage")
                                .Complete();

            return this;
        }

        public InitialiseContentCreationBuilder UpdateContentNodes()
        {
            //Usually I wouldnt explcitly instantiate a class here, I would use DI with an IOC container but due to time, I stuck with this
            IContentNodeUpdateService contentNodeUpdateService = new ContentNodeUpdateService(_contentService);

            var newContentNodes = new List<IContent>();

            var homeNode = _contentService.GetRootContent().FirstOrDefault();

            long totalChildren;

            var children = _contentService.GetPagedChildren(homeNode.Id, 0, 999, out totalChildren);

            newContentNodes.Add(homeNode);
            if(totalChildren != default(long))
                newContentNodes.AddRange(children);

            if (newContentNodes.Any())
            {
                foreach (var contentNode in newContentNodes)
                {
                    contentNodeUpdateService.UpdateContent(contentNode, new ContentNodePropertyData[]
                    {
                        new ContentNodePropertyData("pageTitle", $"{contentNode.Name}"),
                        new ContentNodePropertyData("body", $"<p>This is the {contentNode.Name} page in English</p>")
                    });
                }
            }

            return this;
        }
    }
}

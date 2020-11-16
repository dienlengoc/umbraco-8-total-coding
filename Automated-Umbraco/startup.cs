using Automated_Umbraco.Models;
using Automated_Umbraco.Services;
using Automated_Umbraco.Services.Builders;
using Automated_Umbraco.Services.Interfaces;
using System;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace Automated_Umbraco
{
    public class InitialiseContentCreationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            // Append our component to the collection of Components
            // It will be the last one to be run
            composition.Components().Append<InitialiseContentCreationComponent>();
            composition.Components().Append<ClearCacheComponent>();
        }
    }

    public class InitialiseContentCreationComponent : IComponent
    {

        public InitialiseContentCreationComponent(IContentService contentService, IContentTypeService contentTypeService, IFileService fileService, IDataTypeService dataTypeService)
        {
            var initialiseContentCreationBuilder = new InitialiseContentCreationBuilder(contentService, contentTypeService, fileService, dataTypeService);

            initialiseContentCreationBuilder.
                                            CreateContentTypes().
                                            UpdateContentTypes().
                                            CreateContentNodes().
                                            UpdateContentNodes();
        }
        // initialize: runs once when Umbraco starts
        public void Initialize()
        {
        }

        // terminate: runs once when Umbraco stops
        public void Terminate()
        {
        }
    }

    public class ClearCacheComponent : IComponent
    {

        public ClearCacheComponent(IContentTypeService contentTypeService, AppCaches appCaches)
        {
            appCaches.RuntimeCache.Clear();

            var seoContentType = contentTypeService.Get("seo");
            var homeContentType = contentTypeService.Get("home");
            var pageContentType = contentTypeService.Get("contentPage");

            contentTypeService.Save(seoContentType);
            contentTypeService.Save(homeContentType);
            contentTypeService.Save(pageContentType);
        }
        // initialize: runs once when Umbraco starts
        public void Initialize()
        {
        }

        // terminate: runs once when Umbraco stops
        public void Terminate()
        {
        }
    }

}
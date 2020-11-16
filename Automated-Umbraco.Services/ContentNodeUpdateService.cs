using Automated_Umbraco.Models;
using Automated_Umbraco.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Automated_Umbraco.Services
{
    public class ContentNodeUpdateService : IContentNodeUpdateService
    {
        private readonly IContentService _contentService;

        public ContentNodeUpdateService(IContentService contentService)
        {
            _contentService = contentService;
        }

        public void UpdateContent(IContent content, ContentNodePropertyData[] contentProperties)
        {
            if(contentProperties != null)
            {
                foreach (var propertyData in contentProperties)
                {
                    content.SetValue(propertyData.Name, propertyData.Value);
                }
            }

            _contentService.SaveAndPublish(content);
        }
    }
}

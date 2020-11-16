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
    public class ContentTypeUpdateService : IContentTypeUpdateService
    {

        private readonly IContentTypeService _contentTypeService;
        private readonly IFileService _fileService;
        private readonly IDataTypeService _dataTypeService;

        public ContentTypeUpdateService(IContentTypeService contentTypeService, IFileService fileService, IDataTypeService dataTypeService)
        {
            _contentTypeService = contentTypeService;
            _fileService = fileService;
            _dataTypeService = dataTypeService;
        }

        public void SetAllowedContentTypes(IContentType parentContentType, IContentType[] childContentTypes)
        {
            var allowedContentTypes = parentContentType.AllowedContentTypes.ToList();

            var count = 0;

            foreach (var contentType in childContentTypes)
            {
                allowedContentTypes.Add(new ContentTypeSort(contentType.Id, count));

                count++;
            }

            parentContentType.AllowedContentTypes = allowedContentTypes;

            _contentTypeService.Save(parentContentType);
        }

        public void Update(IContentType contentType, ContentTypeData contentTypeData)
        {
            //Set Composites

            if (contentTypeData.Composites != null)
            {
                foreach (var composite in contentTypeData.Composites)
                {
                    var compositeContentType = _contentTypeService.Get(composite);
                    contentType.AddContentType(compositeContentType);
                }
            }

            //Set Property Types

            if(contentTypeData.Properties != null)
            {
                foreach (var property in contentTypeData.Properties)
                {
                    var dataType = _dataTypeService.GetDataType(property.DataTypeName);

                    var newPropertyType = new PropertyType(dataType, property.Alias);
                    newPropertyType.Name = property.Name;

                    contentType.AddPropertyType(newPropertyType, property.Group);
                }
            }


            //Set Templates
            var masterTemplate = _fileService.GetTemplate("master");
            if(masterTemplate == null)
                masterTemplate = _fileService.CreateTemplateWithIdentity("Master", "master", null);

            if (!string.IsNullOrWhiteSpace(contentTypeData.Template))
            {
                var template = _fileService.CreateTemplateWithIdentity(contentTypeData.Template, contentTypeData.Template, null, masterTemplate);
                _fileService.SaveTemplate(template);

                contentType.SetDefaultTemplate(template);
            }

            _contentTypeService.Save(contentType);
        }
    }
}

using Automated_Umbraco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Automated_Umbraco.Services.Interfaces
{
    public interface IContentTypeUpdateService
    {
        void Update(IContentType contentType, ContentTypeData contentTypeData);
        void SetAllowedContentTypes(IContentType parentContentType, IContentType[] childContentTypes);
    }
}

using Automated_Umbraco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Automated_Umbraco.Services.Interfaces
{
    public interface IContentNodeUpdateService
    {
        void UpdateContent(IContent content, ContentNodePropertyData[] contentProperties);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Automated_Umbraco.Models.ContentTypes
{
    public abstract class Component
    {
        abstract public void AddChild(Component component);
        abstract public ContentType Create(int parentId = -1);
    }
}

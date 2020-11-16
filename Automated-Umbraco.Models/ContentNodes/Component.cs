using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Automated_Umbraco.Models.ContentNodes
{
    public abstract class Component
    {
        abstract public void AddChild(Component component);
        abstract public IContent Create(int parentId = -1);
    }
}

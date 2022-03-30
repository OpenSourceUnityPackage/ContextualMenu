using System.Collections.Generic;

namespace ContextualMenu.Runtime
{
    public interface IHaveOptions
    {
        List<Option> GetOptions();
    }
}
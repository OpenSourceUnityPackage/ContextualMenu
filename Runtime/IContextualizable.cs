using System.Collections.Generic;

namespace ContextualMenuPackage
{
    /// <summary>
    /// IContextualizable is an interface that allow you to define which task your class can process.
    /// Inherit from IContextualizable your class to access to Contextual menu feature.
    /// </summary>
    public interface IContextualizable
    {
        /// <summary>
        /// Get the identifier of the tasks that the class it inherits can handle.
        /// </summary>
        /// <returns></returns>
        public List<string> GetTasks();
    }
}
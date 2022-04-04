using System.Collections.Generic;

namespace ContextualMenuPackage
{
    /// <summary>
    /// Inherit from ITask to define task that contextualizable can handle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITask<T> where T : IContextualizable
    {
        /// <summary>
        /// Invoke task with list of contextualizable that own it
        /// </summary>
        /// <param name="contextualizables"></param>
        public void OnInvoked(List<T> contextualizables);
    }
}
using System.Collections.Generic;

namespace ContextualMenuPackage
{
    /// <summary>
    /// ContextualMenu is a class that allow you to manage task depending on action available by the generic type.
    /// If you combine it with your own UI system and selection, you can process Contextual menu like an RTS.
    /// Architecture of this class is made to be independent as possible and online friendly.
    /// </summary>
    /// <typeparam name="T">
    /// Contextualizable that represent object that you will be stored on ContextualMenu and process tasks.
    /// This object need to inheritance of interface IContextualizable
    /// </typeparam>
    public abstract class AContextualMenu<T> where T : IContextualizable
    {
        protected List<T> m_contextualizable; // ref
        protected readonly Dictionary<string, ITask<T>> m_tasks = new Dictionary<string, ITask<T>>();

        /// <summary>
        /// Use this function to add task associate to its ID.
        /// When the ID is invoked, this task is called
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        public virtual void AddTask(string id, ITask<T> task)
        {
            m_tasks.Add(id, task);
        }
        
		/// <summary>
        /// Use this function to get the task associated to the taskID.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ITask<T> GetTask(string taskId)
        {
            if (m_tasks.ContainsKey(taskId))
            {
                return m_tasks[taskId];
            }
            return null;
        }

        /// <summary>
        /// Allow to invoke task thanks to it's ID.
        /// Task will receive contextualizable that also own this ID.
        /// </summary>
        /// <param name="taskId"></param>
        public abstract void InvokeTask(string taskId);

        /// <summary>
        /// Call this function to get list of task's IDs callable based on contextualizables task.
        /// </summary>
        /// <returns></returns>
        public abstract string[] GetTasks();

        /// <summary>
        /// Set contextualizables to managed contextual menu.
        /// </summary>
        /// <param name="contextualizables">null to clear</param>
        public virtual void SetContextualizable(in List<T> contextualizables)
        {
            m_contextualizable = contextualizables;
        }
    }
}

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
    public class ContextualMenu<T> where T : IContextualizable
    {
        protected List<T> m_contextualizable; // ref

        protected Dictionary<string, ITask<T>> m_tasks = new Dictionary<string, ITask<T>>();
        protected Dictionary<string, List<T>> m_taskOwned = new Dictionary<string, List<T>>();

        /// <summary>
        /// Use this function to add task associate to it's id.
        /// When the ID is invoked, this task is called
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        public void AddTask(string id, ITask<T> task)
        {
            m_tasks.Add(id, task);
        }

        /// <summary>
        /// Allow to invoke task thanks to it's ID.
        /// Task will receive contextualizable that also own this ID.
        /// </summary>
        /// <param name="taskId"></param>
        public void InvokeTask(string taskId)
        {
            if (m_tasks.ContainsKey(taskId) && m_taskOwned.ContainsKey(taskId))
            {
                m_tasks[taskId].OnInvoked(m_taskOwned[taskId]);
            }
        }

        /// <summary>
        /// Call this function to get list of task's IDs callable based on contextualizables task.
        /// </summary>
        /// <returns></returns>
        public string[] GetTasks()
        {
            string[] str = new string[m_taskOwned.Count];
            m_taskOwned.Keys.CopyTo(str, 0);
            return str;
        }

        /// <summary>
        /// Set contextualizables to managed contextual menu base on it.
        /// </summary>
        /// <param name="contextualizables"></param>
        public void SetContextualizable(in List<T> contextualizables)
        {
            m_contextualizable = contextualizables;
            SortTasks();
        }

        /// <summary>
        /// Clean the contextualizable buffer to remove all possible action possible.
        /// </summary>
        public void CleanContextualizable()
        {
            m_taskOwned.Clear();
        }

        /// <summary>
        /// Sort action based on contextualizables. 
        /// </summary>
        protected void SortTasks()
        {
            CleanContextualizable();

            foreach (T observedSelectable in m_contextualizable)
            {
                foreach (string task in observedSelectable.GetTasks())
                {
                    if (!m_taskOwned.ContainsKey(task))
                        m_taskOwned.Add(task, new List<T>());

                    m_taskOwned[task].Add(observedSelectable);
                }
            }
        }
    }
}
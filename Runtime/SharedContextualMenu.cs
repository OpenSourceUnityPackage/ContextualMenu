using System.Collections.Generic;

namespace ContextualMenuPackage
{
    /// <summary>
    /// SharedContextualMenu only handles the shared task of contextualizables
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SharedContextualMenu<T> : AContextualMenu<T> where T : IContextualizable
    {
        private readonly List<string> m_sharedTask = new List<string>();
        
        public override void InvokeTask(string taskId)
        {
            if (m_tasks.ContainsKey(taskId) && m_sharedTask.Contains(taskId))
            {
                m_tasks[taskId].OnInvoked(m_contextualizable);
            }
        }

        public override string[] GetTasks()
        {
            return m_sharedTask.ToArray();
        }

        public override void SetContextualizable(in List<T> contextualizables)
        {
            base.SetContextualizable(contextualizables);
            SortGlobalTasks();
        }

        void SortGlobalTasks()
        {
            m_sharedTask.Clear();
            if (m_contextualizable == null || m_contextualizable.Count == 0)
                return;
            
            foreach (KeyValuePair<string, ITask<T>> task in m_tasks)
            {
                m_sharedTask.Add(task.Key);
            }

            foreach (T observedSelectable in m_contextualizable)
            {
                List<string> tasks = observedSelectable.GetTasks();
                for (int index = 0; index < m_sharedTask.Count;)
                {
                    string sharedTask = m_sharedTask[index];

                    if (!tasks.Contains(sharedTask))
                        m_sharedTask.RemoveAt(index);
                    else
                        ++index;
                }
            }
            
        }
    }
}
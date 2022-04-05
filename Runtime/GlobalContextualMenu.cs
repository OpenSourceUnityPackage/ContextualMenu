using System.Collections.Generic;

namespace ContextualMenuPackage
{
    /// <summary>
    /// GlobalContextualMenu handles all task of contextualizable even if they are not shared by all.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GlobalContextualMenu<T> : AContextualMenu<T> where T : IContextualizable
    {
        private readonly Dictionary<string, List<T>> m_taskOwned = new Dictionary<string, List<T>>();

        public override void InvokeTask(string taskId)
        {
            if (m_tasks.ContainsKey(taskId) && m_taskOwned.ContainsKey(taskId))
            {
                m_tasks[taskId].OnInvoked(m_taskOwned[taskId]);
            }
        }

        public override string[] GetTasks()
        {
            string[] str = new string[m_taskOwned.Count];
            m_taskOwned.Keys.CopyTo(str, 0);
            return str;
        }

        public override void SetContextualizable(in List<T> contextualizables)
        {
            base.SetContextualizable(contextualizables);
            SortTasks();
        }
        
        private void SortTasks()
        {
            m_taskOwned.Clear();

            if (m_contextualizable == null)
                return;
            foreach (T observedSelectable in m_contextualizable)
            {
                List<string> tasks = observedSelectable.GetTasks();
                foreach (string task in tasks)
                {
                    if (!m_taskOwned.ContainsKey(task))
                        m_taskOwned.Add(task, new List<T>());

                    m_taskOwned[task].Add(observedSelectable);
                }
            }
        }
    }
}
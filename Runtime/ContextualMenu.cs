using System.Collections.Generic;
using UnityEngine;

namespace UnitSelectionPackage
{
    public class ContextualMenu<T> where T : MonoBehaviour, IContextualizable
    {
        protected List<T> m_observedSelectables; // ref

        protected Dictionary<string, ITask<T>> m_tasks = new Dictionary<string, ITask<T>>();
        protected Dictionary<string, List<T>> m_taskOwned = new Dictionary<string, List<T>>();

        public void AddTask(string id, ITask<T> task)
        {
            m_tasks.Add(id, task);
        }

        public void InvokeTask(string taskId)
        {
            if (m_tasks.ContainsKey(taskId))
            {
                m_tasks[taskId].OnSelect(m_taskOwned[taskId]);
            }
        }

        public string[] GetTasks()
        {
            string[] str = new string[m_taskOwned.Count];
            m_taskOwned.Keys.CopyTo(str, 0);
            return str;
        }

        void SortActions()
        {
            m_taskOwned.Clear();
            foreach (T observecSelectable in m_observedSelectables)
            {
                foreach (string task in observecSelectable.GetTasks())
                {
                    if (!m_taskOwned.ContainsKey(task))
                        m_taskOwned.Add(task, new List<T>());

                    m_taskOwned[task].Add(observecSelectable);
                }
            }
        }

        public void SetObserver(in List<T> observedSelectable)
        {
            m_observedSelectables = observedSelectable;
            SortActions();
        }
    }
}
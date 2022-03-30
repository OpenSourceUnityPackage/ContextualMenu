using System.Collections.Generic;
using UnityEngine;

namespace ContextualMenu.Runtime
{
    public abstract class ContextualMenu : MonoBehaviour
    {
        // TODO : Use ObjectPool<> to optimize
        // The key can be changed to an int and the int could be added to the Option class
        // TODO : Display dictionary in editor ?
        [SerializeField, EditInPlayModeOnly]
        protected Dictionary<Sprite, List<Option>> buttonEvents = new Dictionary<Sprite, List<Option>>();

        private void AddOptionInternal(Option newOption)
        {
            if (!buttonEvents.ContainsKey(newOption.sprite))
            {
                buttonEvents.Add(newOption.sprite, new List<Option>());
            }

            buttonEvents[newOption.sprite].Add(newOption);
        }

        public void AddOption(Option newOption)
        {
            AddOptionInternal(newOption);
            UpdateOptions();
        }

        public void AddOptions(List<Option> options)
        {
            foreach (Option newOption in options)
            {
                AddOptionInternal(newOption);
            }

            UpdateOptions();
        }

        // Called when the list of options has been modified
        protected abstract void UpdateOptions();

        // Called when the list of options is cleared
        public virtual void Clear()
        {
            buttonEvents.Clear();
        }
    }
}

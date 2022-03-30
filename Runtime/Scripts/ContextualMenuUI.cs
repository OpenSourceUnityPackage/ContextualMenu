using System.Collections.Generic;
using UnityEngine;

namespace ContextualMenu.Runtime
{
    public class ContextualMenuUI : ContextualMenu
    {
        [SerializeField] GameObject buttonPrefab;

        // TODO : ObjectPool<> ?
        List<ContextualMenuUIButton> buttons = new List<ContextualMenuUIButton>();

        void ClearUI()
        {
            foreach (ContextualMenuUIButton button in buttons)
            {
                Destroy(button.gameObject);
            }

            buttons.Clear();
        }

        // Called when the list of options has been modified
        protected override void UpdateOptions()
        {
            ClearUI();
            foreach (List<Option> optionsList in buttonEvents.Values)
            {
                ContextualMenuUIButton button = Instantiate(buttonPrefab).GetComponent<ContextualMenuUIButton>();
                button.optionsList = optionsList;
            }
        }

        // Called when the list of options is cleared
        public override void Clear()
        {
            base.Clear();
            ClearUI();
        }
    }
}

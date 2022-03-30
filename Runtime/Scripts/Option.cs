using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Option 
{
    // Sprite of the option displayed in the contextual menu
    public Sprite sprite;

    // Callback that will call a Server RPC to create a new instruction
    public UnityAction onClick;

    // The prefered slot for this option
    // a negative value means there are no preferences
    public int idealSlotID = -1;
}

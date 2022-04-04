using System.Collections.Generic;
using ContextualMenuPackage;
using UnityEngine;

public class Stop : ITask<Unit>
{
    public void OnInvoked(List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            target.StopMovement();
        }
    }
}
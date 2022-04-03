using System.Collections.Generic;
using UnitSelectionPackage;
using UnityEngine;

public class MoveContext : ITask<Unit>
{
    public void OnSelect(List<Unit> targets)
    {
        Debug.Log("First ask to get point. Next send event to server that try to move each unit at the selected position");
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace UnitSelectionPackage
{
    public interface ITask<T> where T : MonoBehaviour, IContextualizable
    {
        public void OnSelect(List<T> targets);
    }
}
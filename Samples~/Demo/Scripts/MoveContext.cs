using System.Collections.Generic;
using ContextualMenuPackage;
using UnityEngine;

public class MoveContext : ITask<Unit>
{
    private List<Unit> m_targets; // ref

    public void OnInvoked(List<Unit> targets)
    {
        m_targets = targets;
        GameManager.Instance.RequestPosition += OnPositionIndicate;
    }

    void OnPositionIndicate(Vector3 position)
    {
        Vector3 OffsetFromStart;
        int unitCount = m_targets.Count;
        float unitCountSqrt = Mathf.Sqrt(unitCount);
        int NumberOfCharactersRow = (int)unitCountSqrt;
        int NumberOfCharactersColumn = (int)unitCountSqrt + unitCount - NumberOfCharactersRow * NumberOfCharactersRow;
        float Distance = 1f;
        
        OffsetFromStart = new Vector3(NumberOfCharactersRow * Distance / 2f, 0f,
            NumberOfCharactersColumn * Distance / 2f);

        for (int i = 0; i < unitCount; i++)
        {
            int r = i / NumberOfCharactersRow;
            int c = i % NumberOfCharactersRow;
            Vector3 offset = new Vector3(r * Distance, 0f, c * Distance);
            m_targets[i].MoveTo(position + offset - OffsetFromStart);
        }
    }
}
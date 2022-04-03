using System;
using System.Collections.Generic;
using UnitSelectionPackage;
using UnityEngine;
using Object = UnityEngine.Object;

public class Unit : MonoBehaviour, ISelectable, IContextualizable
{
    public ETeam team = ETeam.Team1;
    private bool m_isSelected = false;
    private Material m_materialRef;
    private Color m_baseColor = Color.white;
    
    public List<string> actions;

    #region MonoBehaviour

    private void OnEnable()
    {
        GameManager.Instance.RegisterUnit(team, this);
    }

    private void OnDisable()
    {
        if(gameObject.scene.isLoaded)
            GameManager.Instance.UnregisterUnit(team, this);
    }
    
    #endregion
    
    protected void Awake()
    {
        m_materialRef = GetComponent<Renderer>().material;
        m_baseColor = m_materialRef.color;
    }

    public void SetSelected(bool selected)
    {
        m_isSelected = selected;
        m_materialRef.color = m_isSelected ? Color.yellow : m_baseColor;
    }
    
    public bool IsSelected()
    {
        return m_isSelected;
    }

    public virtual List<string> GetTasks()
    {
        return actions;
    }
}

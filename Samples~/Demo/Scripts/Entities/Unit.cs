using System;
using System.Collections.Generic;
using UnitSelectionPackage;
using ContextualMenuPackage;
using UnityEngine;

public class Unit : MonoBehaviour, ISelectable, IContextualizable
{
    public ETeam team = ETeam.Team1;
    private bool m_isSelected = false;
    private Material m_materialRef;
    private Color m_baseColor = Color.white;

    public List<string> actions;

    private bool isMoving;
    private Vector3 positionToReach;
    public float speed = 2f;
    public float distanceToReach = 0.1f;

    #region MonoBehaviour

    private void OnEnable()
    {
        GameManager.Instance.RegisterUnit(team, this);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded)
            GameManager.Instance.UnregisterUnit(team, this);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Transform selfTransform = transform;
            Vector3 position = selfTransform.position;
            Vector3 posToTarget = positionToReach - position;
            float posToTargetDistance = posToTarget.magnitude;
            Vector3 direction = posToTarget / posToTargetDistance;
            position += speed * Time.fixedDeltaTime * direction;
            selfTransform.position = position;

            isMoving = posToTargetDistance > distanceToReach;
        }
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

    public void StopMovement()
    {
        isMoving = false;
    }
    
    public void MoveTo(Vector3 target)
    {
        isMoving = true;
        positionToReach = target;
    }
}
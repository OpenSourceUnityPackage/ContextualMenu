using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnitSelectionPackage
{
    public class UnitSelection<T> where T : MonoBehaviour, ISelectable
    {
        [System.Serializable]
        public struct Style
        {
            public float thickness;
            public Color fillColor;
            public Color edgeColor; 
        }
        
        public Style style = new Style
        {
            thickness = 2f,
            fillColor = new Color(0.8f, 0.8f, 0.95f, 0.25f),
            edgeColor = new Color(0.8f, 0.8f, 0.95f)
        };
        
        private Vector3 m_cursorPosition;
        private List<T> m_observedSelectable; // ref
        private List<T> m_selectedObj = new List<T>();

        /// <summary>
        /// Is called on selection. i.e on cursor up after select objects
        /// </summary>
        public Action<List<T>> OnSelection;
        
        /// <summary>
        /// Is called on deselection. i.e when you select unit and click in other point
        /// </summary>
        public Action<List<T>> OnDeselection;

        public void OnSelectionBegin(Vector3 cursorScreenPos)
        {
            OnDeselection?.Invoke(m_selectedObj);
            
            m_cursorPosition = cursorScreenPos;
        }

        public void OnSelectionProcess(Camera camera, Vector3 cursorScreenPos)
        {
            foreach (T unit in m_selectedObj)
            {
                unit.SetSelected(false);
            }
            
            m_selectedObj.Clear();
            foreach (T unit in m_observedSelectable)
            {
                if (IsWithinSelectionBounds(camera, unit.gameObject, cursorScreenPos))
                    m_selectedObj.Add(unit);
            }

            foreach (T unit in m_selectedObj)
            {
                unit.SetSelected(true);
            }
        }
        
        public void OnSelectionEnd()
        {
            OnSelection?.Invoke(m_selectedObj);
        }

        public void DrawGUI(Vector3 cursorScreenPos)
        {
            // Create a rect from both cursor positions
            Rect rect = Utils.GetScreenRect(m_cursorPosition, cursorScreenPos);
            Utils.DrawScreenRect(rect, style.fillColor);
            Utils.DrawScreenRectBorder(rect, style.thickness, style.edgeColor);
        }

        public void SetObserver(in List<T> bufferToObserve)
        {
            m_observedSelectable = bufferToObserve;
        }
        
        public bool IsWithinSelectionBounds(Camera camera, GameObject gameObject, Vector3 cursorScreenPos)
        {
            Bounds viewportBounds =
                Utils.GetViewportBounds(camera, m_cursorPosition, cursorScreenPos);

            return viewportBounds.Contains(
                camera.WorldToViewportPoint(gameObject.transform.position));
        }
    }
}
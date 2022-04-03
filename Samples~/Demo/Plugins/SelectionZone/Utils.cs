using UnityEngine;

namespace UnitSelectionPackage
{
    /// <summary>
    /// <see cref="https://hyunkell.com/blog/rts-style-unit-selection-in-unity-5/"/>
    /// </summary>
    public static class Utils
    {
        static Texture2D m_whiteTexture;

        public static Texture2D WhiteTexture
        {
            get
            {
                if (m_whiteTexture == null)
                {
                    m_whiteTexture = new Texture2D(1, 1);
                    m_whiteTexture.SetPixel(0, 0, Color.white);
                    m_whiteTexture.Apply();
                }

                return m_whiteTexture;
            }
        }

        public static void DrawScreenRect(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
            GUI.color = Color.white;
        }

        public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
        {
            // Top
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            // Left
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            // Right
            DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
            // Bottom
            DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
        }

        public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
        {
            Vector3 v1 = camera.ScreenToViewportPoint(screenPosition1);
            Vector3 v2 = camera.ScreenToViewportPoint(screenPosition2);
            Vector3 min = Vector3.Min(v1, v2);
            Vector3 max = Vector3.Max(v1, v2);
            min.z = camera.nearClipPlane;
            max.z = camera.farClipPlane;

            Bounds bounds = new Bounds();
            bounds.SetMinMax(min, max);
            return bounds;
        }

        public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
        {
            // Move origin from bottom left to top left
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;
            // Calculate corners
            Vector3 topLeft = Vector3.Min(screenPosition1, screenPosition2);
            Vector3 bottomRight = Vector3.Max(screenPosition1, screenPosition2);
            // Create Rect
            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }
    }
}
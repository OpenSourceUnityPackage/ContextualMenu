using UnityEditor;
using UnityEngine;

namespace ContextualMenu.Runtime
{
    // Aims to tell the programmer he should not set the variable from the inspector, 
    // but can modify it at playtime to test things
    public class EditInPlayModeOnly : PropertyAttribute
    {

    }

    [CustomPropertyDrawer(typeof(EditInPlayModeOnly))]
    public class EditInPlayModeOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            GUI.enabled = Application.isPlaying;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}